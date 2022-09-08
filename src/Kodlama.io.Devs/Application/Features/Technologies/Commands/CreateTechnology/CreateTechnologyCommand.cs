using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
{
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;

        public CreateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository)
            => (_mapper, _technologyRepository) = (mapper, technologyRepository);

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology technology = _mapper.Map<Technology>(request);

            Technology createdTechnology = await _technologyRepository.AddAsync(technology);

            CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

            return createdTechnologyDto;
        }
    }
}
