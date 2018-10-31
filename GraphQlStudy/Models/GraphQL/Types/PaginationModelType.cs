using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class PaginationModelType: InputObjectGraphType<PaginationModel>
    {
        #region Constructor

        public PaginationModelType()
        {
            Field(x => x.Page).Description("Record page.");
            Field(x => x.Records).Description("Total records per page.");
        }

        #endregion
    }
}