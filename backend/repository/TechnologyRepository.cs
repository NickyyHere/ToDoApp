using Microsoft.EntityFrameworkCore;
using Npgsql;
using ToDoApp.exception;
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
            try
            {
                _context.Technologies.Add(technology);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) when (e.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                throw new DuplicateEntityException($"Technology {technology.Name} already exists");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Technologies.FindAsync(id) ?? throw new ItemNotFoundException($"Technology with id {id} doesn't exist");
            _context.Technologies.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Technology>> GetAllAsync()
        {
            return await _context.Technologies.ToListAsync();
        }

        public async Task<Technology> GetByIdAsync(int id)
        {
            return await _context.Technologies.FindAsync(id) ?? throw new ItemNotFoundException($"Technology with id {id} doesn't exist");
        }

        public async Task UpdateAsync(int id, Technology newTechnology)
        {
            var item = await _context.Technologies.FindAsync(id) ?? throw new ItemNotFoundException($"Technology with id {id} doesn't exist");;
            _context.Entry(item).CurrentValues.SetValues(newTechnology);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names)
        {
            List<Technology> technologies = await _context.Technologies.Where(t => names.Contains(t.Name)).ToListAsync();
            if(technologies.Count == 0)
            {
                throw new ItemNotFoundException($"Couldn't find any technology that match");
            }
            return technologies;
        }
    }
}