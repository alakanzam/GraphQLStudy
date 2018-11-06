using System.Collections.Generic;
using System.Linq;
using GraphQlStudy.Models;
using GraphQlStudy.Models.GraphQL.Types;

namespace GraphQlStudy.ViewModels
{
    public class SearchClassModel
    {
        #region Properties

        private List<int> _ids;

        public List<int> Ids
        {
            get => _ids;
            set
            {
                if (value == null)
                {
                    _ids = null;
                    return;
                }

                _ids = value.Where(x => x > 0).Distinct().OrderBy(x => x).ToList();
            }
        }

        public RangeModel<double?, double?> OpeningHour { get; set; }
        
        #endregion
        

    }
}