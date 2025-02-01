using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities
{
    public class Phrase : AuditableEntity, IEntity
    {
        public long Id { get; set; }
        public required string Description { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
