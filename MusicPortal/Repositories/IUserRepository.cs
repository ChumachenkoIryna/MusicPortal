using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsersList();
        bool UserExists(int id);
        void Update(User u);
        IQueryable<User> CheckUser(LoginModel logon);


        IQueryable<User> CheckRegisterUser(RegisterModel reg);

        User GetUser(int? id);
        void Create(User item);
        void Delete(int id);
        void Save();
    }
}
