using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Core.Application.Pipelines.Authorization;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Attributes;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

[Authorize(Roles = new[] { "admin" })]
public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>, ISecuredRequest
{
    public string Name { get; set; }

    public class
        CreateProgrammingLanguageHandler : IRequestHandler<CreateProgrammingLanguageCommand,
            CreatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public CreateProgrammingLanguageHandler(IProgrammingLanguageRepository programmingLanguageRepository,
            IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            => (_programmingLanguageRepository, _mapper, _programmingLanguageBusinessRules) = (
                programmingLanguageRepository, mapper, programmingLanguageBusinessRules);

        public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            await _programmingLanguageBusinessRules.ProgrammingLanguageCanNotBeDuplicatedWhenInserted(request.Name);

            ProgrammingLanguage programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);

            ProgrammingLanguage createdProgrammingLanguage =
                await _programmingLanguageRepository.AddAsync(programmingLanguage);

            CreatedProgrammingLanguageDto createdProgrammingLanguageDto =
                _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);
            return createdProgrammingLanguageDto;
        }
    }
}