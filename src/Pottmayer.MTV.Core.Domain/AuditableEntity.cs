using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;

namespace Pottmayer.MTV.Core.Domain
{
    public class AuditableEntity
    {
        public required long CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public User Creator { get; set; } = null!;

        public required long UpdatedBy { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public User Updater { get; set; } = null!;
    }
}
