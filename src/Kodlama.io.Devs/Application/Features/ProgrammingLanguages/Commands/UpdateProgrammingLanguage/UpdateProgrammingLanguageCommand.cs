﻿using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

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
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(p =>
                p.Id == request.Id);

            _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            //programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);

            programmingLanguage.Name = request.Name;

            ProgrammingLanguage updatedProgrammingLanguage = await _programmingLanguageRepository
                .UpdateAsync(programmingLanguage);

            UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = _mapper
                .Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

            return updatedProgrammingLanguageDto;
        }
    }
}