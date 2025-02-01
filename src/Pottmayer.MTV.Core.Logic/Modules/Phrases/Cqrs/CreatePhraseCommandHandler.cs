using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Constants;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Dtos;
using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using Tars.Contracts.Adapter.Cache;
using Tars.Contracts.Adapter.UserProvider;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Logic.Modules.Phrases.Cqrs
{
    public class CreatePhraseCommandHandler : AbstractCommandHandler<CreatePhraseCommand, CreatePhraseOutputDto>, ICreatePhraseCommandHandler
    {
        protected const string EMPTY_DESCRIPTION_MESSAGE = "Description cannot be empty.";
        protected const string NO_PERMISSION_MESSAGE = "Only admin users can create phrases.";

        protected readonly AppDbContext _dbContext;
        protected readonly IUserProvider<UserDataDto> _userProvider;
        protected readonly ICacheService _cacheService;

        public CreatePhraseCommandHandler(AppDbContext dbContext, IUserProvider<UserDataDto> userProvider, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _userProvider = userProvider;
            _cacheService = cacheService;
        }

        protected override async Task<CreatePhraseOutputDto> HandleAsync(CreatePhraseCommand request, CancellationToken cancellationToken)
        {
            if (_userProvider!.User?.Role != UserRole.Admin)
            {
                return new CreatePhraseOutputDto()
                {
                    CreatedPhrase = null,
                    Success = false,
                    Errors = [NO_PERMISSION_MESSAGE]
                };
            }

            try
            {
                if (string.IsNullOrEmpty(request.Input.Description))
                {
                    return new CreatePhraseOutputDto()
                    {
                        CreatedPhrase = null,
                        Success = false,
                        Errors = [EMPTY_DESCRIPTION_MESSAGE]
                    };
                }

                var newPhrase = new Phrase()
                {
                    Description = request.Input.Description,
                    Author = request.Input.Author,
                    IsVisible = request.Input.IsVisible,
                    CreatedBy = _userProvider.User?.Id is not null ? _userProvider.User.Id : KnownUserCodes.SYSTEM_USER_CODE,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = _userProvider.User?.Id is not null ? _userProvider.User.Id : KnownUserCodes.SYSTEM_USER_CODE,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dbContext.Phrases.AddAsync(newPhrase);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _cacheService.Remove(KnownCacheKeys.ALL_PHRASES);

                return new CreatePhraseOutputDto()
                {
                    Success = true,
                    CreatedPhrase = newPhrase
                };
            }
            catch (Exception)
            {
                return new CreatePhraseOutputDto()
                {
                    Success = false,
                    CreatedPhrase = null,
                    Errors = new List<string>() { "Failed to create phrase." }
                };
            }
        }
    }
}