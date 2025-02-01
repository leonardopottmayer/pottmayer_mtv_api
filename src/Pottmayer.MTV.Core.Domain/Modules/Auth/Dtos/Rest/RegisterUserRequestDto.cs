using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Rest
{
    public class RegisterUserRequestDto : IDataTransferObject
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmation { get; set; }
    }
}
