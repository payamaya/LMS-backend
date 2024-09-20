using LMS.Models.Entities;

namespace LMS.Contracts
{
    public interface IActivityTypeRepository
    {
        Task<ActivityType?> GetActivityTypeAsync(Guid id, bool trackChanges);
        Task<IEnumerable<ActivityType>> GetActivityTypesAsync(bool trackChanges);

        Task CreateAsync(ActivityType entity);
        void Update(ActivityType entity);
        void Delete(ActivityType entity);
    }
}