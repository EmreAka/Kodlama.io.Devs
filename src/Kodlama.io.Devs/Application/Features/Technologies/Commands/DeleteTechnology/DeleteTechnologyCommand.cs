using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
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
        public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            => (_mapper, _technologyRepository) = (mapper, technologyRepository);

        public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology technology = await _technologyRepository.GetAsync(t => t.Id == request.Id);

            //business rules

            Technology deletedTechnology = await _technologyRepository.DeleteAsync(_mapper.Map(request, technology));

            return _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
        }
    }
}
