using Application.Features.Developers.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Developers.Queries.GetListUser;

[Authorize(Roles = new[] { "admin" })]
public class GetUserListQuery : PageRequest, IRequest<UserListModel>, ISecuredRequest
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUserListQuery, UserListModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserRepository userRepository, IMapper mapper)
            => (_userRepository, _mapper) = (userRepository, mapper);

        public async Task<UserListModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetListAsync(
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim),
                index: request.Page,
                size: request.PageSize);

            var userListModel = _mapper.Map<UserListModel>(users);

            return userListModel;
        }
    }
}