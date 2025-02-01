using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Users.Entities
{
    public class User : IEntity
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordSalt { get; set; }
        public required UserRole Role { get; set; }
        public ICollection<Phrase> CreatedPhrases { get; set; } = new List<Phrase>();
        public ICollection<Phrase> UpdatedPhrases { get; set; } = new List<Phrase>();
    }
}
