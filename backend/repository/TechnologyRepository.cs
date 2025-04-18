using Microsoft.EntityFrameworkCore;
using ToDoApp.models;
using ToDoApp.repository.dbcontext;
using ToDoApp.repository.interfaces;

namespace ToDoApp.repository
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
        public async Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names)
        {
            return await _context.Technologies.Where(t => names.Contains(t.Name)).ToListAsync();
        }
    }
}