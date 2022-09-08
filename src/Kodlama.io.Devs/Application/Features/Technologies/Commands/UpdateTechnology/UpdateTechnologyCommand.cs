using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;

        public UpdateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository)
            => (_mapper, _technologyRepository) = (mapper, technologyRepository);

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology technology = await _technologyRepository.GetAsync(t => t.Id == request.Id);
            //business
            var updatedTechnology = await _technologyRepository.UpdateAsync(_mapper.Map(request, technology!));

            UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
            return updatedTechnologyDto;
        }
    }
}
