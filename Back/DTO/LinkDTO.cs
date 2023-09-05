using System.Reflection.Emit;

namespace s10.Back.DTO
{
    public class LinkDTO
    { 
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Type { get; private set; }

        public LinkDTO(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
    }
}
