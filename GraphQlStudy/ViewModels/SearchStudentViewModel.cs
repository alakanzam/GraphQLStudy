using GraphQlStudy.Models;

namespace GraphQlStudy.ViewModels
{
    public class SearchStudentViewModel
    {
        #region Properties

        public bool IncludeClasses { get; set; }

        public PaginationModel Pagination { get; set; }

        #endregion
    }
}