using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQlStudy.Interfaces;
using GraphQlStudy.Models;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQlStudy.Domains
{
    public class ClassDomain : IClassDomain
    {
        #region Properties

        private readonly RelationalDbContext _relationalDbContext;

        #endregion

        #region Constructor

        public ClassDomain(DbContext dbContext)
        {
            _relationalDbContext = (RelationalDbContext) dbContext;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Load classes that student takes part in.
        /// </summary>
        /// <param name="studentIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ILookup<int, Class>> LoadClassesByStudentId(IEnumerable<int> studentIds, CancellationToken cancellationToken)
        {
            var participatedClasses = _relationalDbContext.StudentInClasses.AsQueryable();
            var classes = _relationalDbContext.Classes.AsQueryable();

            var loadClassesResult = await (from participatedClass in participatedClasses
                from oClass in classes
                where participatedClass.ClassId == oClass.Id && studentIds.Contains(participatedClass.StudentId)
                select new LoadClassByStudentIdResultModel
                {
                    StudentId = participatedClass.StudentId,
                    Class = oClass
                }).ToListAsync(cancellationToken);

            return loadClassesResult.ToLookup(x => x.StudentId, x => x.Class);

        }

        #endregion
    }
}