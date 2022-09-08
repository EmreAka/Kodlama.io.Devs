using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;

public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
{
    public int Id { get; set; }

    public class
        GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery,
            ProgrammingLanguageGetByIdDto>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository,
            IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            => (_programmingLanguageRepository, _mapper, _programmingLanguageBusinessRules) = (
                programmingLanguageRepository, mapper, programmingLanguageBusinessRules);

        public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request,
            CancellationToken cancellationToken)
        {
            ProgrammingLanguage programmingLanguage =
                await _programmingLanguageRepository.GetAsync(p => p.Id == request.Id);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            ProgrammingLanguageGetByIdDto programmingLanguageGetByIdDto =
                _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);

            return programmingLanguageGetByIdDto;
        }
    }
}