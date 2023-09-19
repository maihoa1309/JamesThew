using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Submission : Base
    {

        public int? ContestId { get; set; }
        [ForeignKey("ContestId")]
        public Contest? GetContest { get; set; }
        public string? UserId { get; set; }
        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe? GetRecipe { get; set; }
        public string? Status { get; set; }
        public int? Point { get; set; }



    }
}
