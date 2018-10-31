using System.Collections.Generic;
using System.Linq;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraphQlStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        #region Properties

        private readonly RelationalDbContext _relationalDbContext;

        #endregion

        #region Constructor

        public StudentController(DbContext relationalDbContext)
        {
            _relationalDbContext = (RelationalDbContext) relationalDbContext;
        }

        #endregion

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromQuery] SearchStudentViewModel condition)
        {
            var students = _relationalDbContext.Students.AsQueryable();
            var classes = _relationalDbContext.Classes.AsQueryable();
            var participatedClasses = _relationalDbContext.StudentInClasses.AsQueryable();

            var bIsClassIncluded = condition.IncludeClasses;

            var result = from student in students
                select new StudentViewModel
                {
                    Id = student.Id,
                    Age = student.Age,
                    FullName = student.FullName,
                    Photo = student.Photo,
                    Classes = !bIsClassIncluded ? null : from participatedClass in participatedClasses
                        from oClass in classes
                        where participatedClass.StudentId == student.Id && participatedClass.ClassId == oClass.Id
                        select new ClassViewModel
                        {
                            Id = oClass.Id,
                            OpeningHour = oClass.OpeningHour,
                            ClosingHour = oClass.ClosingHour,
                            Name = oClass.Name
                        }
                };

            return Ok(result);
        }
        
    }
}