using System.Collections.Generic;
using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class SearchClassModelType : InputObjectGraphType<SearchClassModel>
    {
        public SearchClassModelType()
        {
            Field(x => x.Ids, true).Description("List of class ids");
            Field<RangeModelType<double?, double?>>().Name(nameof(SearchClassModel.OpeningHour)).Description("Opening hour time range");
            //Field(x => x.ClosingHour, true).Description("Class closing hour");
            //Field(x => x.OpeningHour, true).Description("Class opening hour");
            //Field(x => x.Pagination, true).Description("Pagination").Type(new PaginationModelType());
        }
    }
}