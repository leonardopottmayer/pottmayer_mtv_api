using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Repositories;
using Tars.Adapter.Data.Impl;

namespace Pottmayer.MTV.Adapter.Data.Modules.Phrases.Repositories
{
    internal class PhraseRepository : AbstractStandardRepository<Phrase>, IPhraseRepository
    {
        public PhraseRepository() { }
    }
}
