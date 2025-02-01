using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic
{
    public class RegisterUserOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public User? User { get; set; }
    }
}
