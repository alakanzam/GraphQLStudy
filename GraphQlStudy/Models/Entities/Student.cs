using System.Collections.Generic;
using Newtonsoft.Json;

namespace GraphQlStudy.Models.Entities
{
    public class Student
    {
        #region Properties

        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }

        #endregion

        #region Relationships

        [JsonIgnore]
        public virtual ICollection<StudentInClass> StudentsInClasses { get; set; }

        #endregion
    }
}