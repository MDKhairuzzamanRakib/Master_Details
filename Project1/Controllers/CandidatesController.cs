using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

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


    }
}
