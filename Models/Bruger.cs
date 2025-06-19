using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace SpilAPI.Models
{
    public class Bruger
    {
        [Key] public int BrugerId { get; set; }

        [Required]
        public string Brugernavn { get; set; }

        [Required]
        public string Password { get; set; }

       
    }
}

