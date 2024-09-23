using LMS.Infrastructure.Dtos;

namespace LMS.Service.Contracts
{
    public interface IActivityTypeService
    {
        Task<ActivityTypeDto?> GetActivityTypeAsync(Guid courseId, bool trackChanges = false); 
        Task<IEnumerable<ActivityTypeDto>> GetActivityTypesAsync(bool trackChanges = false);
    }
}
