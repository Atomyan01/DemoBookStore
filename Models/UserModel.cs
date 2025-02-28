namespace DemoBookStore.Models
{
    public class UserModel:PersonModel
    {
        public List<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public int Age { get; set; }
        public string Address { get; set; }

        public List<OrderModel> OrderS { get; set; }
        
    }
}
