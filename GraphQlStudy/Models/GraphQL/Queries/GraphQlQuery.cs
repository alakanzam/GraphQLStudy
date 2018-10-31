﻿using Newtonsoft.Json.Linq;

namespace GraphQlStudy.Models.GraphQL.Queries
{
    public class GraphQlQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}