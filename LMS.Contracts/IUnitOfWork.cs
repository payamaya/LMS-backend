namespace LMS.Contracts
{
    public interface IUnitOfWork
    {
     /*   IMovieRepository Movie { get; }
        IActorRepository Actor { get; }*/

        ICourseRepository Course { get; }
        Task CompleteAsync();
    }
}
