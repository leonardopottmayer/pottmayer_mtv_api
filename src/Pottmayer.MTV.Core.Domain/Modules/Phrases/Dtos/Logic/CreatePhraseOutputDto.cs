using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class CreatePhraseOutputDto : IDataTransferObject
    {
        public Phrase? CreatedPhrase { get; set; }
    }
}
