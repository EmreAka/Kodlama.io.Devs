using Application.Features.Technologies.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Technologies.Models;

public class TechnologyListModel
{
    public IPaginate<TechnologyListDto> Items { get; set; }
}