using Microsoft.EntityFrameworkCore;
using System.Linq;
using Teknorix_test.Data;
using Teknorix_test.Models;

namespace Teknorix_test.Services
{
    public interface IJobService
    {
        Task<Response<GetJobDTO>> GetJob(int id);

        Task<Response<GetJobsDTO>> GetJobs(JobQueryDTO jobQueryDTO);

        Task<Response<Object>> AddJob(BaseJobDTO addJobDTO);

        Task<Response<Object>> UpdateJob(BaseJobDTO UpdateJobDTO, int jobId);
    };

    public class JobService : IJobService
    {
        private readonly TeknorixTestContext _TTDB;

        public JobService(TeknorixTestContext TeknorixTestDataContext) { _TTDB = TeknorixTestDataContext; }

        public async Task<Response<GetJobDTO>> GetJob(int id)
        {
            GetJobDTO? job = await _TTDB.Jobs.Where(x => x.JobId == id)
                .Select(x => new GetJobDTO
                {
                    Id = x.JobId,
                    Code = x.JobCode,
                    Title = x.Title,
                    Description = x.Description,
                    Department = x.Department == null ? null : new GetDepartmentDTO
                    {
                        Id = x.DepartmentId.GetValueOrDefault(),
                        Title = x.Department.DepartmentName
                    },
                    Location = new GetLocationDTO
                    {
                        Id = x.CompanyLocationId,
                        City = x.CompanyLocation.City,
                        Title = x.CompanyLocation.Title,
                        Country = x.CompanyLocation.State.Country.CountryName,
                        State = x.CompanyLocation.State.StateName,
                        Zip = x.CompanyLocation.Zip
                    },
                    PostedDate = x.PostedDate,
                    ClosingDate = x.ClosingDate
                })
                .FirstOrDefaultAsync();

            return job is null

                ? throw new Exception("The job has either been archived or it does not exists")

                : new Response<GetJobDTO>() { Success = true, Message = "Sucessfully retrieved the job", Data = job };
        }

        public async Task<Response<GetJobsDTO>> GetJobs(JobQueryDTO jobQueryDTO)
        {
            var jobs = _TTDB.Jobs.AsQueryable();

            int totalRecords = await jobs.CountAsync();

            if (jobQueryDTO.DepartmentId.HasValue) jobs = jobs.Where(x => x.DepartmentId == jobQueryDTO.DepartmentId).AsQueryable();

            if (jobQueryDTO.LocationId.HasValue) jobs = jobs.Where(x => x.DepartmentId == jobQueryDTO.LocationId).AsQueryable();

            if (jobQueryDTO.PageNo.HasValue && jobQueryDTO.PageSize.HasValue) jobs = jobs
                    .OrderBy(x => x.PostedDate)
                    .Skip(((jobQueryDTO.PageNo - 1) * jobQueryDTO.PageSize) ?? 0)
                    .Take(jobQueryDTO.PageSize ?? 10).AsQueryable();

            List<JobListDTO> filteredJobs = await jobs.Select(x => new JobListDTO
            {
                Id = x.JobId,
                Code = x.JobCode,
                Title = x.Title,
                Description = x.Description,
                Department = x.Department == null ? "" : x.Department.DepartmentName,
                Location = x.CompanyLocation.Title,
                PostedDate = x.PostedDate,
                ClosingDate = x.ClosingDate
            }).ToListAsync();

            return new Response<GetJobsDTO>() { Success = true, Message = "Successfully retrieved the job", Data = new GetJobsDTO { Jobs = filteredJobs, Total = totalRecords } };

        }

        public async Task<Response<Object>> AddJob(BaseJobDTO addJobDTO)
        {

            // Logic to check if the provided locationId belongs to the logged in client goes here

            Job job = new()
            {
                DepartmentId = addJobDTO.DepartmentId,
                Description = addJobDTO.Description,
                ClosingDate = DateOnly.FromDateTime(addJobDTO.ClosingDate),
                CompanyLocationId = addJobDTO.LocationId,
                JobCode = "ClientInitials" + addJobDTO.LocationId.ToString() + new Random().Next(1000, 9999), // temporary logic to create JobCode
                PostedDate = DateOnly.FromDateTime(DateTime.Now),
                Title = addJobDTO.Title,
            };

            await _TTDB.Jobs.AddAsync(job);

            await _TTDB.SaveChangesAsync();

            return new Response<Object>() { Data = new { job.JobId }, Message = "Successfully added the job", Success = true };
        }

        public async Task<Response<Object>> UpdateJob(BaseJobDTO updateJobDTO, int jobId)
        {

            // Logic to check if the provided locationId belongs to the logged in client goes here

            var job = await _TTDB.Jobs.Where(x => x.JobId == jobId).FirstOrDefaultAsync(); // Also need to add a filter for companyId based on the singed in client

            if (job == null) throw new Exception("The job has either been archived or it does not exists");

            else
            {
                job.ClosingDate = DateOnly.FromDateTime(updateJobDTO.ClosingDate);
                job.CompanyLocationId = updateJobDTO.LocationId;
                job.DepartmentId = updateJobDTO.DepartmentId;
                job.Description = updateJobDTO.Description;
                job.Title = updateJobDTO.Title;
            }

            await _TTDB.SaveChangesAsync();

            return new Response<Object>() { Message = "Successfully added the job", Success = true };
        }
    }
}
