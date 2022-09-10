using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Technologies.Rules;

public class TechnologyBusinessRules
{
    private readonly ITechnologyRepository _technologyRepository;
    public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        => _technologyRepository = technologyRepository;

    public void TechnologyShouldExistToDelete(Technology technology)
    {
        if (technology == null) throw new BusinessException("This technology doesn't exist");
    }
}