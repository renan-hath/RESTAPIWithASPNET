using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;
using RESTWithNET8.Models.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RESTWithNET8.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var password = ComputeHash(user.Password, SHA256.Create());

            return _context.Users.FirstOrDefault(contextUser =>
            (contextUser.UserName == user.UserName) && (contextUser.Password == password));
        }

        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(contextUser =>
            (contextUser.UserName == username));
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(contextUser =>
            (contextUser.UserName == username));

            if (user is null) 
            {
                return false;
            }

            user.RefreshToken = null;

            _context.SaveChanges();

            return true;
        }

        private object ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            var stringBuilder = new StringBuilder();

            foreach (var hashedByte in hashedBytes)
            {
                stringBuilder.Append(hashedByte.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public User RefreshUserInfo(User user)
        {
            if (!(_context.Users.Any(p => p.Id.Equals(user.Id))))
            {
                return null;
            }

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return result;
        }
    }
}
