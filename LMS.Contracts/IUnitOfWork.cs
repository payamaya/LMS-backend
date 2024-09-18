using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Contracts
{
    public interface IUnitOfWork
    {
     /*   IMovieRepository Movie { get; }

        IActorRepository Actor { get; }*/

        Task CompleteAsync();
    }
}
