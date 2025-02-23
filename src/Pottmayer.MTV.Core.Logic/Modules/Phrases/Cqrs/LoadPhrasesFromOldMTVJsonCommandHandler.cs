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
    public class LoadPhrasesFromOldMTVJsonCommandHandler : AbstractCommandHandler<LoadPhrasesFromOldMTVJsonCommand, LoadPhrasesFromOldMTVJsonOutputDto>, ILoadPhrasesFromOldMTVJsonCommandHandler
    {
        protected readonly IAppDbContext _dbContext;
        protected readonly IUserProvider<UserDataDto> _userProvider;
        protected readonly ICacheService _cacheService;

        public LoadPhrasesFromOldMTVJsonCommandHandler(IAppDbContext dbContext, IUserProvider<UserDataDto> userProvider, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _userProvider = userProvider;
            _cacheService = cacheService;
        }

        protected override async Task<ICommandResult<LoadPhrasesFromOldMTVJsonOutputDto>> HandleAsync(LoadPhrasesFromOldMTVJsonCommand request, CancellationToken cancellationToken)
        {
            if (_userProvider!.User?.Role != UserRole.Admin)
                return Fail(new LoadPhrasesFromOldMTVJsonOutputDto() { });

            foreach (LoadPhrasesFromOldMTVJsonItemDto item in request.Input.Phrases)
            {
                var newPhrase = new Phrase()
                {
                    Description = item.Phrase,
                    Author = item.Author,
                    IsVisible = true,
                    CreatedBy = _userProvider.User?.Id is not null ? _userProvider.User.Id : KnownUserCodes.SYSTEM_USER_CODE,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = _userProvider.User?.Id is not null ? _userProvider.User.Id : KnownUserCodes.SYSTEM_USER_CODE,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _dbContext.AddAsync(newPhrase);
            }

            await _dbContext.SaveChangesAsync();

            _cacheService.Remove(KnownCacheKeys.ALL_PHRASES);

            return Success(new LoadPhrasesFromOldMTVJsonOutputDto() { });
        }
    }
}
