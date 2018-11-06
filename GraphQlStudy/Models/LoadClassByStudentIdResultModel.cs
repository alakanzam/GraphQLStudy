using GraphQlStudy.Models.Entities;

namespace GraphQlStudy.Models
{
    public class LoadClassByStudentIdResultModel
    {
        public int StudentId { get; set; }

        public Class Class { get; set; }

    }
}