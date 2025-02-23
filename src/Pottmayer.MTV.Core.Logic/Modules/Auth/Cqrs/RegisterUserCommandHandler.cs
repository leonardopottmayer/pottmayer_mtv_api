using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Enums;
using System.Text.RegularExpressions;
using Tars.Contracts.Adapter.Authorization;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Logic.Modules.Auth.Cqrs
{
    public class RegisterUserCommandHandler : AbstractCommandHandler<RegisterUserCommand, RegisterUserOutputDto>, IRegisterUserCommandHandler
    {
        protected const string ALREADY_IN_USE_MESSAGE = "{0} is already in use.";
        protected const string FAILED_TO_REGISTER_USER_MESSAGE = "Failed to register user.";
        protected const string PASSWORD_CANNOT_BE_EMPTY_MESSAGE = "Password cannot be empty.";
        protected const string PASSWORD_CONFIRMATION_CANNOT_BE_EMPTY_MESSAGE = "Password confirmation cannot be empty.";
        protected const string PASSWORDS_DO_NOT_MATCH_MESSAGE = "Passwords do not match.";
        protected const string PASSWORDS_ARE_NOT_STRONG_ENOUGH_MESSAGE = "Password is not strong enough.";

        protected readonly AppDbContext _dbContext;

        protected readonly IAuthService _authService;
        protected readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(AppDbContext dbContext, IAuthService authService, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        protected override async Task<ICommandResult<RegisterUserOutputDto>> HandleAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool usernameInUse = await UsernameIsAlreadyInUse(request.Input.Username);
            if (usernameInUse)
                return Fail(new RegisterUserOutputDto() { }, string.Format(ALREADY_IN_USE_MESSAGE, "Username"));

            bool emailInUse = await EmailIsAlreadyInUse(request.Input.Email);
            if (emailInUse)
                return Fail(new RegisterUserOutputDto() { }, string.Format(ALREADY_IN_USE_MESSAGE, "E-mail"));

            var (isPasswordValid, passwordValidationMessage) = IsPasswordValid(request.Input.Password, request.Input.PasswordConfirmation);
            if (!isPasswordValid)
                return Fail(new RegisterUserOutputDto() { }, passwordValidationMessage);

            User? insertedUser = await CreateUser(request.Input);
            if (insertedUser is null)
                return Fail(new RegisterUserOutputDto() { }, FAILED_TO_REGISTER_USER_MESSAGE);

            return Fail(new RegisterUserOutputDto() { User = insertedUser });
        }

        protected async Task<User?> CreateUser(RegisterUserInputDto input)
        {
            string hashedPassword = _passwordHasher.HashPassword(input.Password, out byte[] passwordSalt);

            var newUser = new User
            {
                Id = default,
                Name = input.Name,
                Username = input.Username,
                Email = input.Email,
                Role = UserRole.Default,
                Password = hashedPassword,
                PasswordSalt = Convert.ToBase64String(passwordSalt),
            };

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        protected async Task<bool> UsernameIsAlreadyInUse(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        protected async Task<bool> EmailIsAlreadyInUse(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        protected (bool, string?) IsPasswordValid(string password, string passwordConfirmation)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                return (false, PASSWORD_CANNOT_BE_EMPTY_MESSAGE);

            if (string.IsNullOrEmpty(passwordConfirmation) || string.IsNullOrWhiteSpace(passwordConfirmation))
                return (false, PASSWORD_CONFIRMATION_CANNOT_BE_EMPTY_MESSAGE);

            if (password != passwordConfirmation)
                return (false, PASSWORDS_DO_NOT_MATCH_MESSAGE);

            var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$");
            bool strongPassword = regex.IsMatch(password);

            if (!strongPassword)
                return (false, PASSWORDS_ARE_NOT_STRONG_ENOUGH_MESSAGE);

            return (true, null);
        }
    }
}
