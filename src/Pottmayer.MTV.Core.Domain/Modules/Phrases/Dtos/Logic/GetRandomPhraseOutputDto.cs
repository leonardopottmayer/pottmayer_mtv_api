using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class GetRandomPhraseOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public Phrase? Phrase { get; set; }
    }
}
