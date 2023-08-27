using Project3.Models;

namespace Project3.Models
{
    public class Register : Base
    {
        public int? UserId { get; set; }
        public string? TypeMembership { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
