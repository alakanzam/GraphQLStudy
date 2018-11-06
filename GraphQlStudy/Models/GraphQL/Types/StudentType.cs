using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQlStudy.Interfaces;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class StudentType : ObjectGraphType<StudentViewModel>
    {
        #region Constructor

        /// <summary>
        /// Student graphql entity.
        /// </summary>
        /// <param name="dataLoaderContextAccessor"></param>
        /// <param name="classDomain"></param>
        public StudentType(IDataLoaderContextAccessor dataLoaderContextAccessor, IClassDomain classDomain)
        {
            Name = nameof(Student);

            Field(x => x.Id).Description("Student id.");
            Field(x => x.FullName).Description("Student full name.");
            Field(x => x.Age).Description("Student age");

            Field<ListGraphType<ClassType>, IEnumerable<Class>>()
                .Name(nameof(StudentViewModel.Classes))
                .Description("List of classes which student takes part in")
                .ResolveAsync(context =>
                {
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, Class>(nameof(IClassDomain.LoadClassesByStudentId), classDomain.LoadClassesByStudentId); ;
                    return loader.LoadAsync(context.Source.Id);
                });
            
        }

        #endregion
    }
}