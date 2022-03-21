using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Doktor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Prezime {get;set;}


        //ForeignKeys
        [JsonIgnore]
        public Odeljenje Odeljenje { get; set;}
        [JsonIgnore]

        public List<Spoj> Spojevi {get; set;}

    }
}