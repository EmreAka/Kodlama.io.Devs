using Application.Features.Technologies.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Technologies.Models;

public class TechnologyListModel : BasePageableModel
{
    public IList<TechnologyListDto> Items { get; set; }
}