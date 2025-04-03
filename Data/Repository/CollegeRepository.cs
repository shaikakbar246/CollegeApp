
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbcontext;
        private DbSet<T> _dbSet;
        public CollegeRepository(CollegeDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();
        }

        public async Task<T> CreateAsync(T dbrecord)
        {
            _dbSet.AddAsync(dbrecord);
            await _dbcontext.SaveChangesAsync();
            return dbrecord;
        }

        public async Task<bool> DeleteAsync(T dbrecord)
        {
            _dbSet.Remove(dbrecord);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            //if (useNoTracking)
            //    return await _dbcontext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();
            //else
            //    return await _dbcontext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }
        public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();

        }
        public async Task<T> UpdateAsync(T dbrecord)
        {
            _dbSet.Update(dbrecord);
            await _dbcontext.SaveChangesAsync();
            return dbrecord;
        }
    }
}
