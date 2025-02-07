
namespace DemoBookStore.Models
{
    public class AuthorModel:PersonModel
    {
        public double AverageScore { get; set; }
        public List<AwardModel> Awards { get; set; } = new List<AwardModel>();
        public List<BookModel> Books { get; set; } = new List<BookModel>();

    }
}
