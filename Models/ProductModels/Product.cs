namespace FPT_Ebook.Models.ProductModels
{
    public class Product
    {
        public int BookId { get; set; }

        public string? Title { get; set; }

        public decimal? Price { get; set; }

        public int? QuantityInStock { get; set; }

        public string? ImagePath { get; set; }

        public int? CategoryId { get; set; }

    }
}
