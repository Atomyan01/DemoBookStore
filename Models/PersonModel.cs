using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace DemoBookStore.Models
{
    public class PersonModel:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        
    }
}
