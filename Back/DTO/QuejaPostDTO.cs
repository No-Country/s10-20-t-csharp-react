﻿namespace s10.Back.DTO
{
    public class QuejaPostDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public IFormFile media { get; set; } = null!;
        public int District_ID { get; set; }
        public int Category_ID { get; set; }
        public int User_ID { get; set; }
    }
}
