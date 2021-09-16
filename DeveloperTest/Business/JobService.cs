using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /* In normal circumstances I would use AutoMapper, but since this took my too
        much time already, I've decided to do manual mapping. */
        public JobModel[] GetJobs()
        {
            return context.Jobs.Select(job => new JobModel
            {
                JobId = job.JobId,
                Engineer = job.Engineer,
                Customer = new CustomerModel()
                {
                    CustomerId = job.Customer.CustomerId,
                    Name = job.Customer.Name,
                    Type = job.Customer.Type,
                },
                When = job.When
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs.Where(x => x.JobId == jobId).Select(job => new JobModel
            {
                JobId = job.JobId,
                Engineer = job.Engineer,
                Customer = new CustomerModel()
                {
                    CustomerId = job.Customer.CustomerId,
                    Name = job.Customer.Name,
                    Type = job.Customer.Type,
                },
                When = job.When
            }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                Customer = context.Customers.Single(customer => customer.CustomerId == (int)model.Customer.CustomerId),
                When = model.When
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                Customer = new CustomerModel()
                {
                    CustomerId = addedJob.Entity.Customer.CustomerId,
                    Name = addedJob.Entity.Customer.Name,
                    Type = addedJob.Entity.Customer.Type,
                },
                When = addedJob.Entity.When
            };
        }
    }
}
