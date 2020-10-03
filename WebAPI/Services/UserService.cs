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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Services
{
    public class UserService
    {
        private CryptoFolioContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<Settings> _settings;

        public const int saltSize = 128; // size in bytes
        public const int hashSize = 64; // size in bytes
        public const int iterations = 100000; // number of pbkdf2 iterations

        public UserService(CryptoFolioContext context, IMapper mapper, IOptions<Settings> settings)
        {
            _context = context;
            _mapper = mapper;
           _settings = settings;
        }

        public TokenObject Authenticate(string email, string password, string role, string firstName, string lastName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier, firstName),
                    new Claim(ClaimTypes.Name,lastName),

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

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(u => u.Email == Email);

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

            // Generate Salt

            passwordSalt = new Byte[saltSize]; // Creates new instance   
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            provider.GetBytes(passwordSalt);

            // Generate Hash

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, passwordSalt, iterations);
            passwordHash = pbkdf2.GetBytes(hashSize);
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            //validation

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, iterations);
            var passwordHash = pbkdf2.GetBytes(hashSize);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != storedHash[i])
                    return false;
            }

            return true;

        }
    }
}
