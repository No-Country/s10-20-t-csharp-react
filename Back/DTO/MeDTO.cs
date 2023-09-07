namespace s10.Back.DTO
{
    public class MeGetDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string? Picture_Url { get; set; }
        public string? Address { get; internal set; }
    }

    public class MePatchDto
    {
        public string? Name { get; set; }
        public string? GivenName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; internal set; }
    }
}
