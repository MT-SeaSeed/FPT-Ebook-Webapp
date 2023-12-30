using Microsoft.AspNetCore.Mvc;

namespace FPT_Ebook.ViewModels
{
    public class BookDetails
    {
        public int BookId { get; set; }

        public string? Title { get; set; }

        public string? ImagePath { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? QuantityInStock { get; set; }

        public int? CategoryId { get; set; }

    }
}
