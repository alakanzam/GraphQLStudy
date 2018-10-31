using System.Threading.Tasks;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraphQlStudy.Controllers
{
    [Route("graphql")]
    public class GraphQlController : Controller
    {
        #region Properties

        private readonly RelationalDbContext _relationalDbContext;

        #endregion

        #region Contructor

        public GraphQlController(DbContext dbContext)
        {
            _relationalDbContext = (RelationalDbContext)dbContext;
        }

        #endregion

        #region Methods

        [HttpPost("")]
        public async Task<IActionResult> Query([FromBody] GraphQlQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema()
            {
                Query = new BasicStudentSearchQuery(_relationalDbContext)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
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