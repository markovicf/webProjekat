using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Bolnica
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }
        
    
        [Required]
        [MaxLength(100)]
        public string Adresa{get;set;}
       

        //ForeignKeys
        [JsonIgnore]
        public List<Odeljenje> Odeljenja { get; set; }

    }
}