using Newtonsoft.Json.Linq;

namespace GraphQlStudy.Models.GraphQL
{
    /// <summary>
    /// GraphQL query
    /// </summary>
    public class GraphQLQuery
    {
        #region Properties

        /// <summary>
        /// Operation name.
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Named query.
        /// </summary>
        public string NamedQuery { get; set; }

        public string Query { get; set; }

        public JObject Variables { get; set; }

        #endregion
    }
}