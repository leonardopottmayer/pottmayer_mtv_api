using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Cqrs
{
    public class RegisterUserCommand : AbstractCommand<RegisterUserInputDto, RegisterUserOutputDto>
    {
        public RegisterUserCommand(RegisterUserInputDto input) : base(input) { }
    }

    public interface IRegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserOutputDto> { }
}