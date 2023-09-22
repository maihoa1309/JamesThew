using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Contest : Base
    {
        public string? Rule { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? GetCategory { get; set; }
        public DateTime? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Img { get; set; }

    }
}
