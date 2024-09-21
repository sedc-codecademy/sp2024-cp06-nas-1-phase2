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
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table;
        }

        public T GetById(int id)
        {
            return table.SingleOrDefault(x => x.Id == id);
        }

        public int Add(T entity)
        {
            table.Add(entity);
            return _context.SaveChanges();
        }

        public int Remove(int id)
        {
            var item = table.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                table.Remove(item);
                return id;
            }

            return _context.SaveChanges();
        }
    }
}
