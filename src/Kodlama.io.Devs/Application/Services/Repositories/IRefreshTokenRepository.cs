﻿using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository: IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
{
    
}