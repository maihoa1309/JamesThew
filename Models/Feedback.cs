using Project3.Models;

namespace Project3.Models
{
    public class Feedback : Base
    {
        public int? UserId { get; set; }
        public string? TypeFeedback { get; set; }
        public string? Content { get; set; }
    }
}
