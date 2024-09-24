namespace LMS.Contracts
{
    public interface IUnitOfWork
    {
        ICourseRepository Course { get; }
        IModuleRepository Module { get; }
        IActivityRepository Activity { get; }
        IActivityTypeRepository ActivityType { get; }
        IUserRepository User { get; }
        Task CompleteAsync();
        //Delete 
        Task SaveAsync();
    }
}
