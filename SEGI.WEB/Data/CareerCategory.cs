namespace SEGI.WEB.Data
{
    public class CareerCategory
    {
        public int CareerId { get; set; }
        public Career Career { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
