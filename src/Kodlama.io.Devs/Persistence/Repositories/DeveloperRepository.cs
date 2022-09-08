using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DeveloperRepository : EfRepositoryBase<Developer, BaseDbContext>, IDeveloperRepository
{
    public DeveloperRepository(BaseDbContext context) : base(context)
    {
    }
}
