using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly MusicContext _context;

        public UserRepository(MusicContext context)
        {
            _context = context;
        }
        public  List<User> GetUsersList()
        {
             return  _context.Users.ToList();
        }

        public IQueryable<User> CheckUser(LoginModel logon)
        {
                return _context.Users.Where(a => a.Username == logon.Username);
        }

        public IQueryable<User> CheckRegisterUser(RegisterModel reg)
        {
            return _context.Users.Where(a => a.Username == reg.Username);
        }

        public User GetUser(int? Id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == Id);
        }
        public  void Create(User u)
        {
                 _context.Users.Add(u);
                 _context.SaveChanges();
        }

        public void Update(User u)
        {
         //   _context.Entry(u).State = EntityState.Modified;
            _context.Users.Update(u);
            _context.SaveChanges();
        }

        public  void Delete(int id)
        {
            User? u =  _context.Users.Find(id);
            if (u != null)
                _context.Users.Remove(u);
            _context.SaveChanges();
        }

        public  void Save()
        {
             _context.SaveChanges();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

       
    }
}
