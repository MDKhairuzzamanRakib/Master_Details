using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project1.Models;
using Project1.Models.ViewModels;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly CandidateDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CandidatesController(CandidateDbContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        [HttpGet]
        [Route("GetSkills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }
        [HttpGet]
        [Route("GetCandidates")]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidate()
        {
            return await _context.Candidates.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<CandidateSkill>> PostCandidateSkills([FromForm] CandidateVM vm)
        {
            var skillItems = JsonConvert.DeserializeObject<Skill[]>(vm.SkillsStringfy);

            Candidate candidate = new Candidate
            {
                CandidateName = vm.CandidateName,
                BirthDate = vm.BirthDate,
                Fresher = vm.Fresher
            };

            if (vm.PictureFile!=null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.PictureFile.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath,FileMode.Create);
                await vm.PictureFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                candidate.Picture = fileName;
            }
            foreach (var item in skillItems)
            {
                var candidateSkill = new CandidateSkill
                {
                    Candidate = candidate,
                    CandidateId = candidate.CandidateId,
                    SkillId = item.SkillId
                };
                _context.Add(candidateSkill);
            }
            await _context.SaveChangesAsync();
            return Ok(candidate);
        }

    }
}
