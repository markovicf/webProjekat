using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Odeljenje
    {
        [Key]
        public int ID { get; set;}

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set;}
        [Required]
        [MaxLength(30)]
        public string Specijalizacija { get; set;}

        //fk
        [JsonIgnore]
        public Bolnica Bolnica { get; set;}
        [JsonIgnore]
        public List<Doktor> Doktori { get; set;}
    }
}