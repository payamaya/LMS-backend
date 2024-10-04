using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserAsync(Guid id, bool trackChanges)
        {
            string idString = id.ToString();
            return await FindByCondition(a => a.Id.Equals(idString), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(bool onlyTeachers, bool trackChanges)
        {
            if (onlyTeachers)
            {
                return await FindByCondition(u => u.IsStudent == false, trackChanges).ToListAsync();
            }

            return await FindAll(trackChanges).ToListAsync();
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
