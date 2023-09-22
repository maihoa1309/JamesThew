using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTO;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class ContestController : BaseController<Contest>
    {
     
        private readonly IContestRepository _contestRepository;

        public ContestController(IBaseRepository<Contest> repository, IContestRepository contestRepository) : base(repository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<IActionResult> SaveContest([FromBody] Contest request)
        {
            //var result = await _contestRepository.(request);
            var result = await _contestRepository.SaveContestAsync(request);
            return Json(request);
        }
        public async Task<ContestDetailDTO>  GetSubmission (int ContestId, string keyword, int index, int size){
            var result = await _contestRepository.GetSubmissionAsync(ContestId, keyword, index, size);
            return result;
        }
        public async Task<ContestDTO> GetByName( string keyword, int index, int size)
        {
            var result = await _contestRepository.GetByNameAsync(keyword, index, size);
            return result;
        }
    }
}