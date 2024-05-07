using AvBeacon.Domain.Entities;
using AvBeacon.Domain.Interfaces.Repositories;
using AVBEACON.Persistence;

namespace AvBeacon.Persistence.Repositories;

public class JobOfferRepository(ApplicationDbContext context)
    : GenericRepository<JobOffer>(context), IJobOfferRepository;