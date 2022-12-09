namespace MyFirstASPNetCoreAPI.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Alter { get; set; }
        public string? Art { get; set; }
        public string? Rasse { get; set; }
        public bool Geimpft { get; set; }
        public string? Geschlecht { get; set; }
    }
}
