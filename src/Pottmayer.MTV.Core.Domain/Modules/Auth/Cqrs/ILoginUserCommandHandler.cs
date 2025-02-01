using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Cqrs
{
    public class LoginUserCommand : AbstractCommand<LoginUserInputDto, LoginUserOutputDto>
    {
        public LoginUserCommand(LoginUserInputDto input) : base(input) { }
    }

    public interface ILoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserOutputDto> { }
}
