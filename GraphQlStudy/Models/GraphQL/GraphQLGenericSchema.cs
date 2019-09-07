using GraphQL;
using GraphQL.Types;

namespace GraphQlStudy.Queries
{
    public class GraphQLGenericSchema<TQuery> : Schema
    {
        #region Constructor

        /// <summary>
        /// Initialize schema with injectors.
        /// </summary>
        /// <param name="dependencyResolver"></param>
        public GraphQLGenericSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = (IObjectGraphType)dependencyResolver.Resolve(typeof(TQuery));
        }

        #endregion
    }
}