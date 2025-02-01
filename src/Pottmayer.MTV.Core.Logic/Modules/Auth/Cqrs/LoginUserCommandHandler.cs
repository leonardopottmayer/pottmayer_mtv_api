using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;
using Tars.Contracts.Adapter.Authorization;
using Tars.Contracts.Adapter.Authorization.Dtos;
using Tars.Core.Cqrs;
using System.Security.Claims;

namespace Pottmayer.MTV.Core.Logic.Modules.Auth.Cqrs
{
    public class LoginUserCommandHandler : AbstractCommandHandler<LoginUserCommand, LoginUserOutputDto>, ILoginUserCommandHandler
    {
        protected const string INVALID_USERNAME_OR_PASSWORD_MESSAGE = "Invalid username or password.";

        protected readonly AppDbContext _dbContext;

        protected readonly IAuthService _authService;
        protected readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(AppDbContext dbContext, IAuthService authService, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        protected override async Task<LoginUserOutputDto> HandleAsync(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? foundUser = await FindUser(request.Input.Username, request.Input.Email, cancellationToken);
            if (foundUser is null)
                return MountOutputDto(false, INVALID_USERNAME_OR_PASSWORD_MESSAGE, null);

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Input.Password, foundUser.PasswordSalt, foundUser.Password);
            if (!isPasswordValid)
                return MountOutputDto(false, INVALID_USERNAME_OR_PASSWORD_MESSAGE, null);

            ICollection<AuthTicketClaimDto> claims = BuildUserClaims(foundUser);
            IAuthTicket authTicket = _authService.CreateAuthTicket(claims, Convert.ToString(foundUser.Id));

            return MountOutputDto(true, null, authTicket);
        }

        protected async Task<User?> FindUser(string? username, string? email, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower(), cancellationToken);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
            }

            return null;
        }

        protected ICollection<AuthTicketClaimDto> BuildUserClaims(User user)
        {
            ICollection<AuthTicketClaimDto> claims = new List<AuthTicketClaimDto>()
            {
                new AuthTicketClaimDto()
                {
                    ClaimName = nameof(User.Id),
                    ClaimType = ClaimValueTypes.String,
                    ClaimValue = Convert.ToString(user.Id)
                },
                new AuthTicketClaimDto()
                {
                    ClaimName = nameof(User.Username),
                    ClaimType = ClaimValueTypes.String,
                    ClaimValue = Convert.ToString(user.Username)
                },
                new AuthTicketClaimDto()
                {
                    ClaimName = nameof(User.Email),
                    ClaimType = ClaimValueTypes.String,
                    ClaimValue = Convert.ToString(user.Email)
                },
                new AuthTicketClaimDto()
                {
                    ClaimName = nameof(User.Role),
                    ClaimType = ClaimValueTypes.Integer32,
                    ClaimValue = Convert.ToString((int)user.Role)
                }
            };

            return claims;
        }

        protected LoginUserOutputDto MountOutputDto(bool success, string? message, IAuthTicket? authTicket)
            => new LoginUserOutputDto() { Success = success, Message = message, AuthTicket = authTicket };
    }
}
