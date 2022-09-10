using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using Application.Features.Technologies.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
{
    public int Id { get; set; }

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;
        public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository,
            IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            => (_mapper, _technologyRepository, _technologyBusinessRules)
            = (mapper, technologyRepository, technologyBusinessRules);

        public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology technology = await _technologyRepository.GetAsync(t => t.Id == request.Id);

            //business rules
            _technologyBusinessRules.TechnologyShouldExist(technology);

            Technology deletedTechnology = await _technologyRepository.DeleteAsync(_mapper.Map(request, technology));

            return _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
        }
    }
}
