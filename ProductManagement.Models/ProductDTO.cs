namespace ProductManagement.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}