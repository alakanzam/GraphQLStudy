using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQlStudy.Interfaces;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.Entities;
using GraphQlStudy.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GraphQlStudy.Domains
{
    public class ClassDomain : IClassDomain
    {
        private readonly RelationalDbContext _relationalDbContext;

        public ClassDomain(DbContext dbContext)
        {
            _relationalDbContext = (RelationalDbContext) dbContext;
        }

        public async Task<IEnumerable<ClassViewModel>> SearchAsync(SearchClassModel loadClassCondition, CancellationToken cancellationToken = default(CancellationToken))
        {
            var participatedClasses = _relationalDbContext.StudentInClasses.AsQueryable();
            var classes = _relationalDbContext.Classes.AsQueryable();

            if (loadClassCondition != null)
            {
                var classIds = loadClassCondition.Ids;
                classIds = classIds.Where(x => x > 0).Select(x => x).Distinct().ToList();
                if (classIds.Count > 0)
                    classes = classes.Where(x => classIds.Contains(x.Id));

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

            var result = from oClass in classes
                from participatedClass in participatedClasses
                where participatedClass.ClassId == oClass.Id &&
                      participatedClass.StudentId == loadClassCondition.StudentId
                select new ClassViewModel
                {
                    Id = oClass.Id,
                    ClosingHour = oClass.ClosingHour,
                    Name = oClass.Name,
                    OpeningHour = oClass.OpeningHour
                };

            return await result.ToListAsync(cancellationToken);
        }
    }
}