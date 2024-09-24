using AutoMapper;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class ActivityService : IActivityService

    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ActivityService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ActivityDto?> GetActivityAsync(Guid courseId, bool trackChanges = false)
        {

            var activity = await _uow.Activity.GetActivityAsync(courseId, trackChanges);
            if (activity is null) return null; //ToDo: Fix later

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync(bool trackChanges = false)
        {
            var activity = await _uow.Activity.GetActivitiesAsync(trackChanges);
            if (activity is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<ActivityDto>>(activity);

        }

        public async Task<bool> DeleteActivityAsync(Guid id)
        {
            var activity = await _uow.Activity.GetActivityAsync(id, trackChanges: false);
            if (activity == null)
                return false;

            _uow.Activity.Delete(activity);
            await _uow.SaveAsync(); // Assuming you have a SaveAsync method in your Unit of Work
            return true;
        }

    }
}
