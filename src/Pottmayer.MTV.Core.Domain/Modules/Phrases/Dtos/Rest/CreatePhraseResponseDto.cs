using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class CreatePhraseResponseDto : IDataTransferObject
    {
        public PhraseDto? CreatedPhrase { get; set; }
    }
}
