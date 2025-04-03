
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbcontext;

        public StudentRepository(CollegeDBContext dbcontext) : base(dbcontext)
        {

            _dbcontext = dbcontext;

        }
        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            return null;
        }
    }
}
