using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetListTechnology;

public class GetTechnologyListQuery : IRequest<TechnologyListModel>
{
    public PageRequest? PageRequest { get; set; }

    public class GetTechnologyListQueryHandler : IRequestHandler<GetTechnologyListQuery, TechnologyListModel>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;

        public GetTechnologyListQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
            => (_technologyRepository, _mapper) = (technologyRepository, mapper);

        public async Task<TechnologyListModel> Handle(GetTechnologyListQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<Technology> technologies = await _technologyRepository.GetListAsync(
                include: m => m.Include(c => c.ProgrammingLanguage!),
                index: request.PageRequest!.Page,
                size: request.PageRequest.PageSize);

            TechnologyListModel technologyListModel = _mapper.Map<TechnologyListModel>(technologies);

            return technologyListModel;
        }
    }
}