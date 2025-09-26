using Microsoft.EntityFrameworkCore;
using Service.Catalogue.Api.Data;
using Service.Catalogue.Api.DTOs;
using Service.Catalogue.Api.Models;
using System.Runtime.Intrinsics.Arm;

namespace Service.Catalogue.Api.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _dbContext;
        public ServiceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(ServiceCatalogueArgument svc)
        {
            var dto = new ServiceCatalogueDto
            {
                Id = Guid.NewGuid(),
                Name = svc.Name,
                OwnerTeam = svc.OwnerTeam,
                Tier = svc.Tier ?? string.Empty,
                Lifecycle = svc.Lifecycle ?? string.Empty,
                Endpoints = svc.Endpoints ?? new List<string>(),
                Tags = svc.Tags ?? new List<string>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _dbContext.Services.Add(dto);

            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var e = await _dbContext.Services.FindAsync(id);
            if (e == null) return;
            _dbContext.Services.Remove(e);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ServiceCatalogueDto?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Services.FindAsync(id);
        }

        public async Task<ServiceCatalogueDto?> GetByNameAsync(string name)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<ServiceCatalogueDto?>> SearchAsync(string? ownerTeam, string? tier, string? lifecycle, string? search)
        {
            var query = _dbContext.Services.AsQueryable();

            if (!string.IsNullOrEmpty(ownerTeam))
                query = query.Where(s => s.OwnerTeam == ownerTeam);

            if (!string.IsNullOrEmpty(tier))
                query = query.Where(s => s.Tier == tier);

            if (!string.IsNullOrEmpty(lifecycle))
                query = query.Where(s => s.Lifecycle == lifecycle);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search) || s.Tags.Any(t => t.Contains(search)));

            //if (!string.IsNullOrEmpty(search))
            //{
            //    var searchLower = search.ToLower();
            //    query = query.Where(s => s.Name.ToLower().Contains(searchLower) || s.Tags.Contains(searchLower));
            //}

            return await query.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task UpdateAsync(Guid id, ServiceCatalogueArgument svc)
        {
            var exists = await _dbContext.Services.FindAsync(id);

            if (exists == null)
                throw new KeyNotFoundException("Service not found");

            exists.Id = id;
            exists.Name = svc.Name;
            exists.OwnerTeam = svc.OwnerTeam;
            exists.Tier = svc.Tier ?? string.Empty;
            exists.Lifecycle = svc.Lifecycle ?? string.Empty;
            exists.Endpoints = svc.Endpoints ?? new List<string>();
            exists.Tags = svc.Tags ?? new List<string>();
            exists.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}