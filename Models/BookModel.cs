namespace DemoBookStore.Models
{
    public class BookModel
    {
        public int  ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsElectronic { get; set; }
        public bool IsAvailable { get; set; }
        public int? AgeRestriction { get; set; }

    }
}
