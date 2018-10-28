using System.Collections.Generic;
using Newtonsoft.Json;

namespace GraphQlStudy.Models.Entities
{
    public class Class
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public double OpeningHour { get; set; }

        public double ClosingHour { get; set; }

        #endregion

        #region Relationships
        
        [JsonIgnore]
        public virtual ICollection<StudentInClass> StudentsInClasses { get; set; }

        #endregion
    }
}