using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;

public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery,
            ProgrammingLanguageListModel>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository,
            IMapper mapper)
            => (_programmingLanguageRepository, _mapper) = (programmingLanguageRepository, mapper);

        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguage> programmingLanguages =
                await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

            ProgrammingLanguageListModel mappedProgrammingLanguageListModel =
                _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);

            return mappedProgrammingLanguageListModel;
        }
    }
}