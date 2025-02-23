using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Constants;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts.Adapter.Cache;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Logic.Modules.Phrases.Cqrs
{
    public class GetAllPhrasesCommandHandler : AbstractCommandHandler<GetAllPhrasesCommand, GetAllPhrasesOutputDto>, IGetAllPhrasesCommandHandler
    {
        protected readonly IAppDbContext _dbContext;
        protected readonly ICacheService _cacheService;

        public GetAllPhrasesCommandHandler(IAppDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        protected override async Task<ICommandResult<GetAllPhrasesOutputDto>> HandleAsync(GetAllPhrasesCommand request, CancellationToken cancellationToken)
        {
            bool cacheExists = _cacheService.Exists(KnownCacheKeys.ALL_PHRASES);

            var allPhrases = new List<Phrase>();

            if (cacheExists)
            {
                allPhrases = _cacheService.Get<List<Phrase>>(KnownCacheKeys.ALL_PHRASES) ?? new List<Phrase>();
            }
            else
            {
                allPhrases = await _dbContext.Phrases.AsNoTracking().ToListAsync();
                _cacheService.Set(KnownCacheKeys.ALL_PHRASES, allPhrases, TimeSpan.FromMinutes(1));
            }

            return Success(new GetAllPhrasesOutputDto() { Phrases = allPhrases });
        }
    }
}
