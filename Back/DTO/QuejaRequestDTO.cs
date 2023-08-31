using quejapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace quejapp.DTO
{
    public class QuejaRequestDTO : RequestDTO<Queja>
    {        

        [DefaultValue("CreatedAt")]
        public new string? SortColumn { get; set; } = "CreatedAt";
        [DefaultValue("DESC")]
        public new string? SortOrder { get; set; } = "DESC";
        [DefaultValue(null)]
        public new string? FilterQuery { get; set; } = null;
        [DefaultValue(null)]
        public int? Category_ID { get; set; } = null;
        [DefaultValue(null)]
        public int? District_ID { get; set; }
    }
}
