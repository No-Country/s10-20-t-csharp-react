using System.ComponentModel;

namespace quejapp.DTO;

public class CommentByQuejaDTO : RequestDTO<CommentRequestDTO>
{
    [DefaultValue("AddedAt")]
    public string Address { get; set; } = string.Empty;
    public new string? SortColumn { get; set; } = "AddedAt";

}
