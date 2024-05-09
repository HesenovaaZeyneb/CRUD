namespace mvc_1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string MainPhotoUrl { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
