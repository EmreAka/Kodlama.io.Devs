using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IProgrammingLanguageRepository: IAsyncRepository<ProgrammingLanguage>, IRepository<ProgrammingLanguage>
{
}
