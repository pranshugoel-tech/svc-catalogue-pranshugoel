using Service.Catalogue.Api.DTOs;
using Service.Catalogue.Api.Models;
using Service.Catalogue.Api.Repositories;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Catalogue.Api.Services.Implementation
{
    public class ServiceCatalogueService : IServiceCatalogueService
    {
        private readonly IServiceRepository _repo;
        public ServiceCatalogueService(IServiceRepository repo)
        {
            _repo = repo;

        }
        public async Task<IEnumerable<ServiceCatalogue>> SearchAsync(string? ownerTeam, string? tier, string? lifecycle, string? search)
        {
            var data = await _repo.SearchAsync(ownerTeam, tier, lifecycle, search);

            return data.Select(svc => new ServiceCatalogue
            {
                ID = svc.Id,
                Name = svc.Name,
                OwnerTeam = svc.OwnerTeam,
                Tier = svc.Tier,
                Lifecycle = svc.Lifecycle,
                Endpoints = svc.Endpoints,
                Tags = svc.Tags,
                CreatedAt = svc.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'"),
                UpdatedAt = svc.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'")
            });
        }

        public async Task<ServiceCatalogue?> GetByIdAsync(Guid id)
        {
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
                return null;

            return new ServiceCatalogue
            {
                ID = data.Id,
                Name = data.Name,
                OwnerTeam = data.OwnerTeam,
                Tier = data.Tier,
                Lifecycle = data.Lifecycle,
                Endpoints = data.Endpoints,
                Tags = data.Tags,
                CreatedAt = data.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'"),
                UpdatedAt = data.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'")
            };
        }

        public async Task<ServiceCatalogue?> GetByNameAsync(string name)
        {
            var data = await _repo.GetByNameAsync(name);

            if (data == null)
                return null;

            return new ServiceCatalogue
            {
                ID = data.Id,
                Name = data.Name,
                OwnerTeam = data.OwnerTeam,
                Tier = data.Tier,
                Lifecycle = data.Lifecycle,
                Endpoints = data.Endpoints,
                Tags = data.Tags,
                CreatedAt = data.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'"),
                UpdatedAt = data.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ss'Z'")
            };
        }

        public async Task<ServiceCatalogue?> CreateAsync(ServiceCatalogueArgument svc)
        {
            try
            {
                var exists = await _repo.GetByNameAsync(svc.Name);

                if (exists != null)
                    throw new InvalidOperationException("Service with this name already exists.");

                await _repo.CreateAsync(svc);

                return await GetByNameAsync(svc.Name);

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Service with this name already exists. Error: {ex.Message}");
            }
        }

        public async Task<ServiceCatalogue?> UpdateAsync(Guid id, ServiceCatalogueArgument svc)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Service not found.");

            // Ensure name uniqueness
            if (!string.Equals(existing.Name, svc.Name, StringComparison.OrdinalIgnoreCase))
            {
                var byName = await _repo.GetByNameAsync(svc.Name);
                if (byName != null) throw new InvalidOperationException("Service with this name already exists.");
            }

            await _repo.UpdateAsync(id, svc);
            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

    }

}