using AutoMapper;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class ActivityTypeService : IActivityTypeService

    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ActivityTypeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ActivityTypeDto?> GetActivityTypeAsync(Guid courseId, bool trackChanges = false)
        {

            var activityType = await _uow.ActivityType.GetActivityTypeAsync(courseId, trackChanges);
            if (activityType is null) return null; //ToDo: Fix later

            return _mapper.Map<ActivityTypeDto>(activityType);
        }

        public async Task<IEnumerable<ActivityTypeDto>> GetActivityTypesAsync(bool trackChanges = false)
        {
            var activityTypes = await _uow.ActivityType.GetActivityTypesAsync(trackChanges);
            if (activityTypes is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);

        }

    }
}
