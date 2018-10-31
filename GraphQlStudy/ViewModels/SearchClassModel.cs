using System.Collections.Generic;
using GraphQlStudy.Models;
using GraphQlStudy.Models.GraphQL.Types;

namespace GraphQlStudy.ViewModels
{
    public class SearchClassModel
    {
        #region Properties

       public List<int> Ids { get; set; }

        //public RangeModelType<double?, double?> OpeningHour { get; set; }

        //public RangeModelType<double?, double?> ClosingHour { get; set; }

        public PaginationModelType Pagination { get; set; }

        #endregion
    }
}