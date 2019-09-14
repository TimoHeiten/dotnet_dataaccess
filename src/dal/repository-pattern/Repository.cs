using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;
using Microsoft.EntityFrameworkCore;

namespace dal.repositorypattern
{
    public class EfRepository : IRepository<Customers>, IUnitOfWork
    {
        private udemyContext _context;

        public EfRepository(udemyContext context)
        {
            _context = context;
        }
        public Task AddAsync(Customers item)
        {
            return _context.AddAsync(item);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _context = new udemyContext();
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using (var context = new udemyContext())
            {
                return (IEnumerable<Customers>)(await context.Customers.ToListAsync());
            }
        }
    }
}