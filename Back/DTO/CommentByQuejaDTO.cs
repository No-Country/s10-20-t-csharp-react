using System.ComponentModel;

namespace s10.Back.DTO;

public class CommentByQuejaDTO : RequestDTO<CommentRequestDTO>
{
    [DefaultValue("AddedAt")]
    public new string? SortColumn { get; set; } = "AddedAt";

}
