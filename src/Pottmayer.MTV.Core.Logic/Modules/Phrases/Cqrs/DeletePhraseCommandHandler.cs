using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Constants;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Dtos;
using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using Tars.Contracts.Adapter.Cache;
using Tars.Contracts.Adapter.UserProvider;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Logic.Modules.Phrases.Cqrs
{
    public class DeletePhraseCommandHandler : AbstractCommandHandler<DeletePhraseCommand, DeletePhraseOutputDto>, IDeletePhraseCommandHandler
    {
        protected const string PHRASE_NOT_FOUND_MESSAGE = "Phrase not found.";
        protected const string NO_PERMISSION_MESSAGE = "Only admin users can delete phrases.";

        protected readonly IAppDbContext _dbContext;
        protected readonly ICacheService _cacheService;
        protected readonly IUserProvider<UserDataDto> _userProvider;

        public DeletePhraseCommandHandler(IAppDbContext dbContext, ICacheService cacheService, IUserProvider<UserDataDto> userProvider)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
            _userProvider = userProvider;
        }

        protected override async Task<ICommandResult<DeletePhraseOutputDto>> HandleAsync(DeletePhraseCommand request, CancellationToken cancellationToken)
        {
            if (_userProvider!.User?.Role != UserRole.Admin)
                return Fail(new DeletePhraseOutputDto() { }, NO_PERMISSION_MESSAGE);

            Phrase? phrase = await _dbContext.Phrases.FirstOrDefaultAsync(p => p.Id == request.Input.PhraseId);

            if (phrase is null)
            {
                return Fail(new DeletePhraseOutputDto() { }, PHRASE_NOT_FOUND_MESSAGE);
            }

            _dbContext.Phrases.Remove(phrase);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _cacheService.Remove(KnownCacheKeys.ALL_PHRASES);

            return Success(new DeletePhraseOutputDto() { });
        }
    }
}