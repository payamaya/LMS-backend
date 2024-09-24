using LMS.Infrastructure.Dtos;

namespace LMS.Service.Contracts
{
    public interface IActivityService
    {
        Task<ActivityDto?> GetActivityAsync(Guid activityId, bool trackChanges = false);
        Task<IEnumerable<ActivityDto>> GetActivitiesAsync(bool trackChanges = false);

        Task DeleteActivityAsync(Guid id);
    }
}
