using System;
using System.Threading.Tasks;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.GraphQL;
using GraphQlStudy.Queries;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlStudy.Controllers
{
    [Route("graphql")]
    public class GraphQlController : Controller
    {
        #region Properties

        private readonly RelationalDbContext _relationalDbContext;

        private readonly IDependencyResolver _dependencyResolver;

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Contructor

        public GraphQlController(DbContext dbContext, IDependencyResolver dependencyResolver, IServiceProvider serviceProvider)
        {
            _relationalDbContext = (RelationalDbContext)dbContext;
            _dependencyResolver = dependencyResolver;
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        [HttpPost("")]
        public async Task<IActionResult> Query([FromBody] GraphQlQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema(_dependencyResolver)
            {
                Query = new StudentQuery(_relationalDbContext)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.Listeners.Add(_serviceProvider.GetRequiredService<DataLoaderDocumentListener>());
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        #endregion
    }
}