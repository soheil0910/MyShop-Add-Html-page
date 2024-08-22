using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using MyShop.Models;

namespace MyShop.Data.Repositories
{
    public interface IUserRepository
    {
        void addUser(Users user);
        bool IsExistUserByEmail(string Email);
        Users GetUserForLogin(string Email,string Password);

    }

    public class UserRepository : IUserRepository
    {
        MyShopContext _context;

        public UserRepository(MyShopContext context)
        {
            _context = context;
        }

        public void addUser(Users user)
        {
            _context.Users.Add(user);
            //_context.Add(user);
            _context.SaveChanges();
        }

        public bool IsExistUserByEmail(string Email)
        {
            return _context.Users.Any(c => c.Email == Email);
        }

        public Users GetUserForLogin(string Email, string Password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == Email && u.Password== Password);
        }
    }
}
