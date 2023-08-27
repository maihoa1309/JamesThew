using Microsoft.AspNetCore.Identity;

namespace Project3.Models
{
    public class CustomUser : IdentityUser

    {
        public bool? Gender { get; set; }  
    }
}
