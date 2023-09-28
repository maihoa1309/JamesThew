using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTO;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class FeedbackController : BaseController<Feedback>
    {
		private readonly IFeedBackRepository _feedBackRepository;
		public FeedbackController(IBaseRepository<Feedback> repository, IFeedBackRepository feedBackRepository) : base(repository)
        {
			_feedBackRepository = feedBackRepository;

		}
        public async Task<List<FeedbackDetailDTO>> GetFeedbackRecipe (string keyword, int index, int size)
        {

            return await _feedBackRepository.GetFeedbackRecipeAsync(keyword, index, size);

		}
        public async Task<bool> DeleteFeedBack (int id)
        {
            var feedback = _feedBackRepository.FindById(id);
            await _feedBackRepository.DeleteAsync(feedback);
            return true;
        }
        public async Task<bool> SaveFeedback (Feedback req)
        {
            await _feedBackRepository.SaveFeedbackContentAsync(req);
            return true;
        }
    }
}