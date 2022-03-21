using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj
    {
        [Required]
        public int ID {get;set;}
        [Required]

        public DateTime Datum{get;set;}
        //fk
        [JsonIgnore]
        public Doktor Doktor {get;set;}
        [JsonIgnore]
        public Pacijent Pacijent{get;set;}
    }
}