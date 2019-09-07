using System;
using System.Collections.Generic;
using System.Linq;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.Entities;
using GraphQlStudy.Models.GraphQL.Types;
using GraphQlStudy.ViewModels;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQlStudy.Models.GraphQL.Queries
{
    public class ClassQuery : ObjectGraphType
    {
        #region Properties

        private readonly RelationalDbContext _relationalDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize student query.
        /// </summary>
        /// <param name="dbContext"></param>
        public ClassQuery(DbContext dbContext)
        {
            _relationalDbContext = (RelationalDbContext)dbContext;

            var classQueryArguments = new QueryArguments();
            classQueryArguments.Add(new QueryArgument< IntGraphType> { Name = nameof(Student.Id), Description = "Class id" });
            Field<ClassType>("class", arguments: classQueryArguments,
                resolve: LoadStudent, description: "Search for a specific student");

        }



        #endregion

        #region Methods

        /// <summary>
        /// Load a student using specific condition.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Class LoadStudent(ResolveFieldContext<object> context)
        {
            var id = context.Arguments.Where(x => x.Key.Equals(nameof(Class.Id), StringComparison.InvariantCultureIgnoreCase)).Select(x => (int?)x.Value).FirstOrDefault();
            
            var classes = _relationalDbContext.Classes.AsQueryable();
            classes = classes.Where(x => id != null && x.Id == id);

            return classes.FirstOrDefault();
        }

        /// <summary>
        /// Load a list of students using specific conditions.
        /// </summary>
        /// <param name="context"></param>
        protected virtual List<StudentViewModel> LoadStudents(ResolveFieldContext<object> context)
        {
            var students = _relationalDbContext.Students.AsQueryable();

            var ids = context.GetArgument<List<int>>("ids");
            var age = context.GetArgument<RangeModel<double?, double?>>("age");

            if (ids != null)
                students = students.Where(x => ids.Contains(x.Id));

            if (age != null)
            {
                var from = age.From;
                var to = age.To;

                if (from != null)
                    students = students.Where(x => x.Age >= from);

                if (to != null)
                    students = students.Where(x => x.Age <= to);
            }

            var pagination = context.GetArgument<PaginationModel>("pagination");

            var loadedStudents = (from student in students
                                  select new StudentViewModel
                                  {
                                      Id = student.Id,
                                      Age = student.Age,
                                      FullName = student.FullName,
                                      Photo = student.Photo
                                  });

            if (pagination != null)
            {
                var pageIndex = pagination.Page = 1;
                pageIndex -= 1;
                if (pageIndex < 0)
                    pageIndex = 0;

                var records = pagination.Records;
                if (records < 0)
                    records = 0;

                loadedStudents = loadedStudents.Skip(pageIndex * pagination.Records).Take(records);
            }
            return loadedStudents.ToList();
        }
        #endregion
    }
}