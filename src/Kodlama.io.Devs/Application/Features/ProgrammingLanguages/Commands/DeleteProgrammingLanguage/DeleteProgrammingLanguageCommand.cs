using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
{
    public int Id { get; set; }

    public class DeleteProgrammingLanguageComandHandler : IRequestHandler<DeleteProgrammingLanguageCommand,
        DeletedProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public DeleteProgrammingLanguageComandHandler(IMapper mapper,
            IProgrammingLanguageRepository programmingLanguageRepository,
            ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            => (_mapper, _programmingLanguageRepository, _programmingLanguageBusinessRules) =
                (mapper, programmingLanguageRepository, programmingLanguageBusinessRules);

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage =
                await _programmingLanguageRepository.GetAsync(p => p.Id == request.Id);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            //ProgrammingLanguage programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);

            ProgrammingLanguage deletedProgrammingLanguage =
                await _programmingLanguageRepository.DeleteAsync(programmingLanguage);

            DeletedProgrammingLanguageDto deletedProgrammingLanguageDto =
                _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

            return deletedProgrammingLanguageDto;
        }
    }
}