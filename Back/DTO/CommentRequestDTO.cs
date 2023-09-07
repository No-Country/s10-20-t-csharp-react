using System.Diagnostics.CodeAnalysis;

namespace s10.Back.DTO;

public class CommentRequestDTO
{
    // el id del usuario se deduce del token cuando haya login y se carga directamente al modelo oficial de bd
    [NotNull] 
    public string Text { get; set; } = string.Empty;
    //[NotNull]
    //public int Complaint_ID { get; set; }
    // el día llegó
    //public int User_ID { get; set; } // eliminable cuando haya login   
}
