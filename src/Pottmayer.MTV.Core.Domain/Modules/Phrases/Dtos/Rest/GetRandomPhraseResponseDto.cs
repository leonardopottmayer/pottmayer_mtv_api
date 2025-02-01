using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class GetRandomPhraseResponseDto : IDataTransferObject
    {
        public PhraseDto? Phrase { get; set; }
    }
}
