using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        #region Constructor

        public RootQuery()
        {
            Name = "Root";

            Field<StudentQuery>().Name("student");
            Field<ClassQuery>().Name("class");
        }

        #endregion
    }
}