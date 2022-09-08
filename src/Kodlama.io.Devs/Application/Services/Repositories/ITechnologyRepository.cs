using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ITechnologyRepository : IAsyncRepository<Technology>, IRepository<Technology>
{
    
}