namespace SpilAPI.DTO
{
    public class ScoreDto
    {
        public int ScoreId { get; set; }
        public int BrugerId { get; set; }
        public string Brugernavn { get; set; }
        public int SpilId { get; set; }
        public string Spilnavn { get; set; }
        public int Point { get; set; }
        public DateTime Dato { get; set; }
    }

}
