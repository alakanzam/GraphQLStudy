using System.Collections.Generic;

namespace GraphQlStudy.ViewModels
{
    public class StudentViewModel
    {
        #region Properties

        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }
        
        public IEnumerable<ClassViewModel> Classes { get; set; }

        #endregion

    }
}