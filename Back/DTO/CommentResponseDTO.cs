﻿using System.Diagnostics.CodeAnalysis;

namespace quejapp.DTO
{
    public class CommentResponseDTO
    {        
        public int Comment_ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Complaint_ID { get; set; }
        public int? User_ID { get; set; } // eliminable cuando haya login
        public string? UserName { get; set; }
        public DateTime AddedAt { get; set; }
    }
}