using CryptoFolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Services
{
    public class UserService
    {
        private UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
