using System.Threading.Tasks;
using angu.models;

namespace angu.Data
{
    public interface IAuthRespository
    {
         Task<User> RegisterUser(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExist(string username);
    }
}