using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        => _programmingLanguageRepository = programmingLanguageRepository;

    public async Task ProgrammingLanguageCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(b => b.Name.ToLower() == name.ToLower());
        if (result.Items.Any()) throw new BusinessException("Programming language exists.");
    }

    public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage programmingLanguage)
    {
        if (programmingLanguage == null) throw new BusinessException("Requested programming language does not exist");
    }
}
