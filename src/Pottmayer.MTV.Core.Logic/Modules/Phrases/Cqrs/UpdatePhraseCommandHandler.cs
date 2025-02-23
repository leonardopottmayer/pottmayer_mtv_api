using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Constants;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Dtos;
using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using System.Dynamic;
using Tars.Contracts.Adapter.Cache;
using Tars.Contracts.Adapter.UserProvider;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;
using Tars.Core.Utils;

namespace Pottmayer.MTV.Core.Logic.Modules.Phrases.Cqrs
{
    public class UpdatePhraseCommandHandler : AbstractCommandHandler<UpdatePhraseCommand, UpdatePhraseOutputDto>, IUpdatePhraseCommandHandler
    {
        protected const string PHRASE_NOT_FOUND_MESSAGE = "Phrase not found.";
        protected const string PHRASE_DATA_EMPTY_MESSAGE = "Phrase data is empty.";
        protected const string FAILED_TO_UPDATE_PHRASE_MESSAGE = "Failed to update phrase.";
        protected const string NO_PERMISSION_MESSAGE = "Only admin users can update phrases.";

        protected readonly AppDbContext _dbContext;
        protected readonly IUserProvider<UserDataDto> _userProvider;
        protected readonly ICacheService _cacheService;

        public UpdatePhraseCommandHandler(AppDbContext dbContext, IUserProvider<UserDataDto> userProvider, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _userProvider = userProvider;
            _cacheService = cacheService;
        }

        protected override async Task<ICommandResult<UpdatePhraseOutputDto>> HandleAsync(UpdatePhraseCommand request, CancellationToken cancellationToken)
        {
            if (_userProvider!.User?.Role != UserRole.Admin)
                return Fail(new UpdatePhraseOutputDto() { }, NO_PERMISSION_MESSAGE);

            if (request.Input.PhraseData is null)
                return Fail(new UpdatePhraseOutputDto() { }, PHRASE_DATA_EMPTY_MESSAGE);

            Phrase? phrase = await _dbContext.Phrases.FirstOrDefaultAsync(p => p.Id == request.Input.PhraseId);

            if (phrase is null)
                return Fail(new UpdatePhraseOutputDto() { }, PHRASE_NOT_FOUND_MESSAGE);

            Phrase? mergedPhrase = MergeIncomingAndCurrentPhrase(request.Input, phrase, request.Input.PhraseData);

            if (mergedPhrase is null)
                return Fail(new UpdatePhraseOutputDto() { }, FAILED_TO_UPDATE_PHRASE_MESSAGE);

            mergedPhrase.UpdatedBy = _userProvider.User?.Id is not null ? _userProvider.User.Id : KnownUserCodes.SYSTEM_USER_CODE;
            mergedPhrase.UpdatedAt = DateTime.UtcNow;

            _dbContext.Update(mergedPhrase);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _cacheService.Remove(KnownCacheKeys.ALL_PHRASES);

            return Success(new UpdatePhraseOutputDto() { });
        }

        protected Phrase? MergeIncomingAndCurrentPhrase(UpdatePhraseInputDto incoming, Phrase current, ExpandoObject expando)
        {
            var mergeFields = new Dictionary<string, string>() {
                { nameof(UpdatePhraseInputDto.Description), nameof(Phrase.Description) },
                { nameof(UpdatePhraseInputDto.Author), nameof(Phrase.Author) },
                { nameof(UpdatePhraseInputDto.IsVisible), nameof(Phrase.IsVisible) },
            };

            var merger = new ObjectMerger<UpdatePhraseInputDto, Phrase>(incoming, current, (expando as IDictionary<string, object>), mergeFields);
            Phrase? mergedPhrase = merger.MergeObjects();

            return mergedPhrase;
        }
    }
}
