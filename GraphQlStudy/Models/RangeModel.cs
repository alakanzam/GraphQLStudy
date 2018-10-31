using GraphQL.Types;

namespace GraphQlStudy.Models
{
    public class RangeModel<TFrom, TTo>
    {
        #region Propertes

        public TFrom From { get; set; }

        public TTo To { get; set; }

        #endregion
    }
}