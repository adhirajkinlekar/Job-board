using Microsoft.AspNetCore.Mvc;
using Teknorix_test.Data;
using Teknorix_test.Services;

namespace Teknorix_test.Controllers
{
    [Route("api/v1/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet()]
        public async Task<ActionResult<Response<GetJobDTO>>> GetJobs(JobQueryDTO jobQueryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return Ok(await _jobService.GetJobs(jobQueryDTO));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetJobDTO>>> GetJob(int id)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            };

            return Ok(await _jobService.GetJob(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetJobDTO>>> AddJob(BaseJobDTO addJobDTO)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            };

            return Ok(await _jobService.AddJob(addJobDTO));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<GetJobDTO>>> UpdateJob(BaseJobDTO addJobDTO, int id)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            };

            return Ok(await _jobService.UpdateJob(addJobDTO, id));
        }
    }
} 