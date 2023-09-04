using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace s10.Back.DTO
{
    public class RequestDTO<T>
    {
        [DefaultValue(1)]
        public int PageIndex { get; set; } = 1;

        [DefaultValue(10)]
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        [DefaultValue("Name")]
        public string? SortColumn { get; set; } = "Name";

        [DefaultValue("ASC")]
        // [SortOrderValidator]
        public string? SortOrder { get; set; } = "ASC";

        [DefaultValue(null)]
        public string? FilterQuery { get; set; } = null;        
    }
}