using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using GraphQL.DataLoader;

namespace GraphQlStudy.Interfaces
{
    public interface IClassDomain
    {
        #region Methods

        /// <summary>
        /// Load classes that student takes part in.
        /// </summary>
        /// <param name="studentIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ILookup<int, Class>> LoadClassesByStudentId(IEnumerable<int> studentIds, CancellationToken cancellationToken);

        #endregion
    }
}