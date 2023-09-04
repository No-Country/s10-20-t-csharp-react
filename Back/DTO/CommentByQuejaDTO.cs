using System.ComponentModel;

namespace quejapp.DTO;

public class CommentByQuejaDTO : RequestDTO<CommentRequestDTO>
{
    [DefaultValue("AddedAt")]
    public new string? SortColumn { get; set; } = "AddedAt";

}
