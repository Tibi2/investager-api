﻿using Investager.Core.Models;
using System;
using System.Threading.Tasks;

namespace Investager.Core.Services
{
    public interface ICoreUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<RefreshToken> RefreshTokens { get; }

        IGenericRepository<Asset> Assets { get; }

        IGenericRepository<Portfolio> Portfolios { get; }

        IGenericRepository<UserStarredAsset> UserStarredAssets { get; }

        Task SaveChanges();
    }
}
