using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private readonly NewsAggregatorDbContext _context;
        private DbSet<T> table = null;

        public Repository(NewsAggregatorDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await table.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex.InnerException);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                if (id < 0 || id > table.Count())
                {
                    throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                }
                return await table.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await table.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                table.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await table.FindAsync(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                }

                table.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public IEnumerable<T> GetAll()
        //{
        //    return table;
        //}

        //public T GetById(int id)
        //{
        //    return table.SingleOrDefault(x => x.Id == id);
        //}

        //public int Add(T entity)
        //{
        //    table.Add(entity);
        //    return _context.SaveChanges();
        //}

        //public int Remove(int id)
        //{
        //    var item = table.SingleOrDefault(x => x.Id == id);
        //    if (item != null)
        //    {
        //        table.Remove(item);
        //        return id;
        //    }

        //    return _context.SaveChanges();
        //}
    }
}
