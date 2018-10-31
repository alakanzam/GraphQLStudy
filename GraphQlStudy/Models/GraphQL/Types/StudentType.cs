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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GraphQlStudy.Models.GraphQL.Types
{
    public class StudentType : ObjectGraphType<StudentViewModel>
    {
        #region Constructor

        public StudentType(DbContext dbContext, ICacheService<string, IEnumerable<ClassViewModel>> loadClassesCacheService)
        {
            var relationalDbContext = (RelationalDbContext)dbContext;

            Name = nameof(Student);

            Field(x => x.Id).Description("Student id.");
            Field(x => x.FullName).Description("Student full name.");
            Field(x => x.Age).Description("Student age");
            Field<ListGraphType<ClassType>, IEnumerable<ClassViewModel>>()
                .Name(nameof(StudentViewModel.Classes))
                .Description("List of classes which student takes part in")
                .Argument<SearchClassModelType>("class", "Class search condition")
                .ResolveAsync(context =>
                {
                    var loadClassCondition = context.GetArgument<SearchClassModel>("class");
                    var jLoadClassCondition = JsonConvert.SerializeObject(loadClassCondition);
                    if (loadClassesCacheService.HasKey(jLoadClassCondition))
                        return Task.FromResult(loadClassesCacheService.Get(jLoadClassCondition));

                    var participatedClasses = relationalDbContext.StudentInClasses.AsQueryable();
                    var classes = relationalDbContext.Classes.AsQueryable();

                    if (loadClassCondition != null)
                    {
                        var classIds = loadClassCondition.Ids;
                        if (classIds != null)
                        {
                            classIds = classIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
                            if (classIds.Count > 0)
                                classes = classes.Where(x => classIds.Contains(x.Id));
                        }

                        var openingHour = loadClassCondition.OpeningHour;
                        if (openingHour != null)
                        {
                            var from = openingHour.From;
                            var to = openingHour.To;

                            if (from != null)
                                classes = classes.Where(x => x.OpeningHour >= from);

                            if (to != null)
                                classes = classes.Where(x => x.OpeningHour <= to);
                        }
                    }

                    var loadedClasses = (from oClass in classes
                        from participatedClass in participatedClasses
                        where participatedClass.ClassId == oClass.Id &&
                              participatedClass.StudentId == context.Source.Id
                        select new ClassViewModel
                        {
                            Id = oClass.Id,
                            ClosingHour = oClass.ClosingHour,
                            Name = oClass.Name,
                            OpeningHour = oClass.OpeningHour
                        }).ToList();

                    loadClassesCacheService.Set(jLoadClassCondition, loadedClasses);
                    return Task.FromResult((IEnumerable<ClassViewModel>) loadedClasses);
                });
        }

        #endregion
    }
}