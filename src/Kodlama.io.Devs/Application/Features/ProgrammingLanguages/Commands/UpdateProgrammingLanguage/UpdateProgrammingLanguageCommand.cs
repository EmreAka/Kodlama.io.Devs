using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool BypassCache => false;
    public string CacheKey => "programming languages";

    public class UpdateProgrammingLanguageCommandHandler :
        IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public UpdateProgrammingLanguageCommandHandler(IMapper mapper,
            IProgrammingLanguageRepository programmingLanguageRepository,
            ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            => (_mapper, _programmingLanguageRepository, _programmingLanguageBusinessRules) =
                (mapper, programmingLanguageRepository, programmingLanguageBusinessRules);

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p =>
                p.Id == request.Id);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            ProgrammingLanguage updatedProgrammingLanguage = await _programmingLanguageRepository
                .UpdateAsync(_mapper.Map(request, programmingLanguage));

            UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = _mapper
                .Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

            return updatedProgrammingLanguageDto;
        }
    }
}