using MQTEC.Data;
using MQTEC.Models;
using System.Linq;

namespace MQTEC.Repositories
{
    public class UserRepository
    {
        private readonly MQTECDbContext _context;

        public UserRepository(MQTECDbContext context)
        {
            _context = context;
        }

        public void Save(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool ValidateLogin(User user)
        {
            bool isValid = true;
            var myUser = _context.Users
                .FirstOrDefault(u => u.Login.Equals(user.Login) && u.Password.Equals(user.Password));

            if (myUser == null)            
                isValid = false;

            return isValid;
        }
    }
}
