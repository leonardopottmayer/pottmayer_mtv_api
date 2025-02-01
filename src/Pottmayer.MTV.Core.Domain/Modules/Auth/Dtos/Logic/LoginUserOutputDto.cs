using Tars.Contracts;
using Tars.Contracts.Adapter.Authorization;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic
{
    public class LoginUserOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IAuthTicket? AuthTicket { get; set; }
    }
}
