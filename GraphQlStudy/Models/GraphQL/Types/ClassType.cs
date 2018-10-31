using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class ClassType: ObjectGraphType<ClassViewModel>
    {
        #region Constructor

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