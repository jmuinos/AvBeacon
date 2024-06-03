using AvBeacon.Domain.Entities;
using AvBeacon.Domain.Repositories;

namespace AvBeacon.Persistence.Repositories;

public class SkillRepository(ApplicationDbContext context) : GenericRepository<Skill>(context), ISkillRepository;