using Microsoft.EntityFrameworkCore;
using ToDoApp.models.Technology;
using ToDoApp.repository.dbcontext.AppDbContext;
using ToDoApp.repository.interfaces.ITechnologyRepository;

namespace ToDoApp.repository
{
    namespace TechnologyRepository
    {
        public class TechnologyRepository : ITechnologyRepository
        {
            private readonly AppDbContext _context;
            public TechnologyRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(Technology technology)
            {
                _context.Technologies.Add(technology);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var item = await _context.Technologies.FindAsync(id);
                if(item != null)
                {
                    _context.Technologies.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<List<Technology>> GetAllAsync()
            {
                return await _context.Technologies.ToListAsync();
            }

            public async Task<Technology?> GetByIdAsync(int id)
            {
                return await _context.Technologies.FindAsync(id);
            }

            public async Task UpdateAsync(int id, Technology newTechnology)
            {
                var item = await _context.Technologies.FindAsync(id);
                if(item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(newTechnology);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}