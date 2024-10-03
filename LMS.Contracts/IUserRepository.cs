using LMS.Models.Entities;

namespace LMS.Contracts
{
	public interface IUserRepository
	{
		Task<User?> GetUserAsync(Guid id, bool trackChanges);
		Task<IEnumerable<User>> GetUsersAsync(bool onlyTeachers, bool trackChanges);

		Task CreateAsync(User entity);
		void Update(User entity);
		void Delete(User entity);
	}
}