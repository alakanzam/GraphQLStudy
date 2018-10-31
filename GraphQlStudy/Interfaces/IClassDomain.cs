using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQlStudy.ViewModels;

namespace GraphQlStudy.Interfaces
{
    public interface IClassDomain
    {
        #region Methods

        Task<IEnumerable<ClassViewModel>> SearchAsync(SearchClassModel condition, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}