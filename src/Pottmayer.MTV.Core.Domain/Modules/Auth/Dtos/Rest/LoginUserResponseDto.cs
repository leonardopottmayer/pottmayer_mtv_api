using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Rest
{
    public class LoginUserResponseDto : IDataTransferObject
    {
        public required string JwtToken { get; set; }
    }
}
