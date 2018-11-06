using GraphQL;
using GraphQL.Types;

namespace GraphQlStudy.Queries
{
    public class StudentSchema : Schema
    {
        public StudentSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<StudentQuery>();
        }
    }
}