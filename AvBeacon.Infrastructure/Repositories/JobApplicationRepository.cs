using AvBeacon.Domain.JobApplications;

namespace AvBeacon.Infrastructure.Repositories;

public class JobApplicationRepository(ApplicationDbContext context)
    : GenericRepository<JobApplication>(context), IJobApplicationRepository
{
    public async Task<JobApplication?> GetAllByCandidateIdAsync<T>(Guid candidateId,
                                                                   CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(new object?[] { candidateId, cancellationToken }, cancellationToken);
    }

    public async Task<JobApplication?> GetByCandidateIdAndState<T>(
        Guid id, JobApplicationStateEnum jobApplicationStateEnum,
        CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(new object?[] { id, cancellationToken }, cancellationToken);
    }
}