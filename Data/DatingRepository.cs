using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angu.models;
using Microsoft.EntityFrameworkCore;

namespace angu.Data
{
    public class DatingRepository : IDatingRespository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Book> GetBookById(long id)
        {
           var book = await _context.Books.FirstOrDefaultAsync(p => p.Id == id);

           return book;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<User> GetUserById(long id)
        {
            var userReturned = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync( user => user.Id == id);

            return userReturned;

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}