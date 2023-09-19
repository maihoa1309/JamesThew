using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Category : Base
    {
        public string? Name { get; set; }
        public string? Img { get; set; }
    }
}
