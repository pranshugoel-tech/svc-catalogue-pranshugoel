using Service.Catalogue.Api.DTOs;
using Service.Catalogue.Api.Models;

namespace Service.Catalogue.Api.Repositories
{
    public interface IServiceRepository
    {
        Task<ServiceCatalogueDto?> GetByIdAsync(Guid id);
        Task<ServiceCatalogueDto?> GetByNameAsync(string name);
        Task<IEnumerable<ServiceCatalogueDto?>> SearchAsync(string? ownerTeam, string? tier, string? lifecycle, string? q);
        Task CreateAsync(ServiceCatalogueArgument svc);
        Task UpdateAsync(Guid id, ServiceCatalogueArgument svc);
        Task DeleteAsync(Guid id);
    }
}
