using Microsoft.AspNetCore.Identity;

namespace Project3.Models
{
    public class CustomUser : IdentityUser
    {
        public string? Gender { get; set; }
        public string? Name { get; set; }
        public string? Avatar {get ; set; } 
        public int? Age { get; set; }
		public bool? IsDeleted { get; set; }
	}       
}
