using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IDeveloperRepository : IAsyncRepository<Developer>, IRepository<Developer>
{
}
