using System.Diagnostics.CodeAnalysis;

namespace quejapp.DTO;

public class CommentRequestDTO
{
    // el id del usuario se deduce del token cuando haya login y se carga directamente al modelo oficial de bd
    [NotNull] 
    public string Text { get; set; } = string.Empty;
    [NotNull]
    public int Complaint_ID { get; set; }
    public int User_ID { get; set; } // eliminable cuando haya login   
}
