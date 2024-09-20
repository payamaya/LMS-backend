using LMS.Models.Entities;

namespace LMS.Contracts
{
    public interface IActivityRepository
    {
        Task<Activity?> GetActivityAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Activity>> GetActivitiesAsync(bool trackChanges);

        Task CreateAsync(Activity entity);
        void Update(Activity entity);
        void Delete(Activity entity);
    }
}