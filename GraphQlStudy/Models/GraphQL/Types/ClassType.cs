using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class ClassType: ObjectGraphType<Class>
    {
        #region Constructor

        /// <summary>
        /// Class type.
        /// </summary>
        public ClassType()
        {
            Name = nameof(Class);

            Field(x => x.Id).Description("Class id.");
            Field(x => x.Name).Description("Class name");
            Field(x => x.ClosingHour).Description("Opening hour");
            Field(x => x.OpeningHour).Description("Closing hour");
        }

        #endregion
        
    }
}