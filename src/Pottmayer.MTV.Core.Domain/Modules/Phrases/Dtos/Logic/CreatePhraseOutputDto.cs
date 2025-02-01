using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class CreatePhraseOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public Phrase? CreatedPhrase { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
