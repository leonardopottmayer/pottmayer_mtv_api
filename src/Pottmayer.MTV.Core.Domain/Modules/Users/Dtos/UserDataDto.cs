using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Users.Dtos
{
    public class UserDataDto : IDataTransferObject
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required UserRole Role { get; set; }
    }
}
