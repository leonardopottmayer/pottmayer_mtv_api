using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class CreatePhraseRequestDto : IDataTransferObject
    {
        public required string Description { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
