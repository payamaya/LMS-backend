using AutoMapper;
using LMS.Application.Exceptions;
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
            if (activity is null)
                return null; //ToDo: Fix later

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync(bool trackChanges = false)
        {
            var activity = await _uow.Activity.GetActivitiesAsync(trackChanges);
            if (activity is null)
                return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<ActivityDto>>(activity);

        }

        public async Task DeleteActivityAsync(Guid id)
        {
            var activitiy = await GetActivityBy(id)
                   ?? throw new NotFoundException("Acitivity", id);
            _uow.Activity.Delete(activitiy);
            await _uow.CompleteAsync();
        }

        private async Task<Activity?> GetActivityBy(Guid id) =>
            await _uow.Activity.GetActivityAsync(id, trackChanges: false);

        public async Task<ActivityDto?> PostActivityAsync(ActivityPostDto postDto)
        {
            ActivityType? activityType = await _uow.ActivityType.GetActivityTypeAsync(postDto.ActivityTypeId, false)
                ?? throw new NotFoundException("No activity type found", postDto.ActivityTypeId);

            Module? module = await _uow.Module.GetModuleAsync(postDto.ModuleId, false)
                ?? throw new NotFoundException("No module found", postDto.ModuleId);

            Activity activity = _mapper.Map<Activity>(postDto);

            await _uow.Activity.CreateAsync(activity);
            await _uow.CompleteAsync();

            return _mapper.Map<ActivityDto>(activity);
        }
    }
}