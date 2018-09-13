using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyStore.Core.Domain;
using MyStore.Infrastructure.EF;
using MyStore.Infrastructure.Jwt;

namespace MyStore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly MyStoreContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;

        public UserService(MyStoreContext context,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
        }

        public async Task<string> GetRoleAsync(string username)
            => await _context.Users.SingleAsync(u => u.Username == username)
                .ContinueWith(t => t.Result.Role);

        public async Task SignUpAsync(string username, string password, string role)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                throw new Exception($"User: {username} already in use.");
            }

            user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password,
                Role = role ?? "user"
            };
            var passwordHash = _passwordHasher.HashPassword(user, password);
            user.Password = passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SignInAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new Exception($"User: {username} was not found.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password.");
            }
        }

        public async Task<JsonWebToken> CreateTokenAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            var claims = new Dictionary<string, string>
            {
                ["custom"] = "value"
            };

            return _jwtHandler.Create(user.Id, user.Username, user.Role, claims);
        }
    }
}