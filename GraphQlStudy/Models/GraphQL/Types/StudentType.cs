using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class StudentType: ObjectGraphType<StudentViewModel>
    {
        #region Constructor

        public StudentType()
        {
            Name = nameof(Student);

            Field(x => x.Id).Description("Student id.");
            Field(x => x.FullName).Description("Student full name.");
            Field(x => x.Age).Description("Student age");
            Field(x => x.Classes, type: typeof(ListGraphType<ClassType>)).Description("List of took part in classes.");
        }

        #endregion
    }
}