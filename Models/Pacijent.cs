using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Pacijent
    {
        [Key]
        public int ID { get; set;}

        [Required]
        [MaxLength(20)]
        public string Ime {get; set;}
        
        [Required]
        [MaxLength(20)]
        public string Prezime {get; set;}

        [Required]
       [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{{1,3}}\\.[0-9]{{1,3}}\\.[0-9]{{1,3}}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{{2,4}}|[0-9]{{1,3}})(\\]?)$")]
        public string Mail {get;set;}
        

        //ForeignKeys
        [JsonIgnore]
        public List<Spoj> Spojevi {get; set;}

    }
}