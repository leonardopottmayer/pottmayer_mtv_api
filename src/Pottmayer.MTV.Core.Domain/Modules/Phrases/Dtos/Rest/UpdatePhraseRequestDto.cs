using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class UpdatePhraseRequestDto : IDataTransferObject
    {
        public string? Description { get; set; }
        public string? Author { get; set; }
        public bool? IsVisible { get; set; }
    }
}
