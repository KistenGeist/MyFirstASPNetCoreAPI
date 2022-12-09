﻿namespace MyFirstASPNetCoreAPI.Models.DTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Alter { get; set; }
        public string? Art { get; set; }
        public string? Rasse { get; set; }
        public bool Geimpft { get; set; }
        public string? Geschlecht { get; set; }

        public PetDTO(int id, string? name, float alter, string? art, string? rasse, bool geimpft, string? geschlecht)
        {
            Id = id;
            Name = name;
            Alter = alter;
            Art = art;
            Rasse = rasse;
            Geimpft = geimpft;
            Geschlecht = geschlecht;
        }
    }
}
