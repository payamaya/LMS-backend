using LMS.Infrastructure.Dtos;

namespace LMS.Service.Contracts
{
	public interface IUserService
	{
		Task<UserDto?> GetUserAsync(Guid courseId, bool trackChanges = false);
		Task<IEnumerable<UserDto>> GetUsersAsync(bool onlyTeachers = false, bool trackChanges = false);

		Task DeleteUserAsync(Guid id);
	}
}
