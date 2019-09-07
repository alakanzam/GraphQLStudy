using System;
using System.Threading.Tasks;
using GraphQlStudy.Models.GraphQL;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlStudy.Controllers
{
    [Route("graphql")]
    public class GraphQlController : Controller
    {
        #region Contructor

        public GraphQlController(IServiceProvider serviceProvider, IDocumentWriter documentWriter,
            IDocumentExecuter documentExecuter, ISchema schema)
        {
            _schema = schema;
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        [HttpPost("")]
        public async Task<IActionResult> Query([FromBody] GraphQLQuery model)
        {
            var inputs = model.Variables.ToInputs();

            var graphQlExecuteOption = new ExecutionOptions();
            graphQlExecuteOption.Schema = _schema;
            graphQlExecuteOption.Query = model.Query;
            graphQlExecuteOption.OperationName = model.OperationName;
            graphQlExecuteOption.Inputs = inputs;
            graphQlExecuteOption.Listeners.Add(_serviceProvider.GetRequiredService<DataLoaderDocumentListener>());

            var graphQlExecuteResult =
                await new DocumentExecuter().ExecuteAsync(graphQlExecuteOption).ConfigureAwait(false);

            if (graphQlExecuteResult.Errors?.Count > 0)
                return BadRequest();

            return Ok(graphQlExecuteResult);
        }

        #endregion

        #region Properties

        private readonly IServiceProvider _serviceProvider;

        private readonly ISchema _schema;

        #endregion
    }
}