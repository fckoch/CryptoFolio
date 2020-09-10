using AutoMapper;
using CryptoFolioAPI.Data;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;
using CryptoFolioAPI.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Services
{
    public class UserService
    {
        private CryptoFolioContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<Settings> _settings;

        public UserService(CryptoFolioContext context, IMapper mapper, IOptions<Settings> settings)
        {
            _context = context;
            _mapper = mapper;
           _settings = settings;
        }

        public async Task<TokenObject> AuthenticateAsync(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            IQueryable<User> query = _context.Users;
            query = query.Where(x => x.UserName == username);
            if (!await query.AnyAsync())
                return null;


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    //new Claim("Store", user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new TokenObject();
            token.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(c => c.UserId).Include(c => c.Wallet).ThenInclude(c => c.Walletcoins).ThenInclude(c => c.Coin);

            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(c => c.UserId == Id).Include(c => c.Wallet).ThenInclude(c => c.Walletcoins).ThenInclude(c => c.Coin);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string Username)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(u => u.UserName == Username);

            return await query.FirstOrDefaultAsync();
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only returns success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        // Hash methods

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //validation

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            //creating hash and salt

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            //validation

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
