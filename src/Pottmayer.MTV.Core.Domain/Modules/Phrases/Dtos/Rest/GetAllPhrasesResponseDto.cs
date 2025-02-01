using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class GetAllPhrasesResponseDto : IDataTransferObject
    {
        public List<PhraseDto> Phrases { get; set; } = new();
    }
}
