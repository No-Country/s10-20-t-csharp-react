namespace quejapp.DTO
{
    public class QuejaDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? VideoAddress { get; set; }
        public string? PhotoAdress { get; set; }
        public int District_ID { get; set; } // se requiere joints
        public int Category_ID { get; set; } // se requiere joints
        public int User_ID { get; set; } // se requiere joints // temporalmente nullable, hasta que tengamos usuarios
    }
}
