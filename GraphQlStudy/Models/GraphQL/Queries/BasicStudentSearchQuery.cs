using System;
using System.Collections.Generic;
using System.Linq;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.Entities;
using GraphQlStudy.Models.GraphQL.Types;
using GraphQlStudy.ViewModels;
using GraphQL.Types;

namespace GraphQlStudy.Models.GraphQL.Queries
{
    public class BasicStudentSearchQuery : ObjectGraphType
    {
        public BasicStudentSearchQuery(RelationalDbContext relationalDbContext)
        {
            var studentQueryArguments = new QueryArguments();
            studentQueryArguments.Add(new QueryArgument<IntGraphType> { Name = nameof(Student.Id), Description = "Student id" });

            Field<StudentType>(nameof(Student), arguments: studentQueryArguments,
                resolve: context =>
                {
                    var id = context.Arguments.Where(x => x.Key.Equals(nameof(Student.Id), StringComparison.InvariantCultureIgnoreCase)).Select(x => (int?) x.Value).FirstOrDefault();
                    
                    var students = relationalDbContext.Students;
                    var classes = relationalDbContext.Classes;
                    var participatedClasses = relationalDbContext.StudentInClasses;

                    var result = (from student in students
                        where student.Id == id
                        select new StudentViewModel
                        {
                            Id = student.Id,
                            Age = student.Age,
                            FullName = student.FullName,
                            Photo = student.Photo,
                            Classes = from participatedClass in participatedClasses
                                from oClass in classes
                                where participatedClass.StudentId == student.Id &&
                                      participatedClass.ClassId == oClass.Id
                                select new ClassViewModel
                                {
                                    Id = oClass.Id,
                                    ClosingHour = oClass.ClosingHour,
                                    Name = oClass.Name,
                                    OpeningHour = oClass.OpeningHour
                                }
                        }).FirstOrDefault();

                    return result;
                });

            var studentsQueryArguments = new QueryArguments();
            studentsQueryArguments.Add(new QueryArgument<ListGraphType<IntGraphType>> { Name = "ids", Description = "Student indexes."});
            studentsQueryArguments.Add(new QueryArgument<RangeModelType<double?, double?>> {Name = "age", Description = "Age range of student."});
            studentsQueryArguments.Add(new QueryArgument<PaginationModelType>{Name = "pagination", Description = "Pagination information"});
            studentsQueryArguments.Add(new QueryArgument<SearchClassModelType>{Name = "class", Description = "Class search condition."});

            Field<ListGraphType<StudentType>>(
                "students",
                arguments: studentsQueryArguments,
                resolve: context =>
                {
                    var students = relationalDbContext.Students.AsQueryable();
                    var classes = relationalDbContext.Classes.AsQueryable();
                    var participatedClasses = relationalDbContext.StudentInClasses.AsQueryable();
                    
                    var ids = context.GetArgument<List<int>>("ids");
                    var age = context.GetArgument<RangeModel<double?, double?>>("age");

                    if (ids != null)
                        students = students.Where(x => ids.Contains(x.Id));

                    var loadClassCondition = context.GetArgument<SearchClassModelType>("class");

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
                            Photo = student.Photo,
                            Classes = from participatedClass in participatedClasses
                                from oClass in classes
                                where participatedClass.StudentId == student.Id &&
                                      participatedClass.ClassId == oClass.Id
                                select new ClassViewModel
                                {
                                    Id = oClass.Id,
                                    ClosingHour = oClass.ClosingHour,
                                    Name = oClass.Name,
                                    OpeningHour = oClass.OpeningHour
                                }
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
                    return loadedStudents;
                });
        }
        
    }
}