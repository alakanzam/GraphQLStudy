using Newtonsoft.Json;

namespace GraphQlStudy.Models.Entities
{
    public class StudentInClass
    {
        #region Properties

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public double JoinedTime { get; set; }

        #endregion

        #region Relationship

        [JsonIgnore]
        public Student Student { get; set; }

        [JsonIgnore]
        public Class Class { get; set; }

        #endregion
    }
}