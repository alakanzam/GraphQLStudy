using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class SearchClassModelType : ObjectGraphType<SearchClassModel>
    {
        public SearchClassModelType()
        {
            Field(x => x.Ids, true).Description("List of class ids");
            //Field(x => x.ClosingHour, true).Description("Class closing hour");
            //Field(x => x.OpeningHour, true).Description("Class opening hour");
            //Field(x => x.Pagination, true).Description("Pagination").Type(new PaginationModelType());
        }
    }
}