using System.ComponentModel.DataAnnotations;
using System;

namespace SpilAPI.Models
{
    public class Score
    {
        [Key] public int ScoreId { get; set; }

        [Required]
        public int BrugerId { get; set; }
        public Bruger Bruger { get; set; }

        [Required]
        public int SpilId { get; set; }

        public Spil Spil { get; set; }

        [Required]
        public int Point { get; set; }

        [Required]
        public DateTime Dato { get; set; } = DateTime.UtcNow;
    }
}

