using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos
{
    public class PhraseDto : IDataTransferObject
    {
        public long Id { get; set; }
        public required string Description { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }
        public required long CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required long UpdatedBy { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
