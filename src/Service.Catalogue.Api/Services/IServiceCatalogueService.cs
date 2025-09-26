using Service.Catalogue.Api.DTOs;
using Service.Catalogue.Api.Models;

namespace Service.Catalogue.Api.Services
{
    public interface IServiceCatalogueService
    {
        Task<IEnumerable<ServiceCatalogue>> SearchAsync(string? ownerTeam, string? tier, string? lifecycle, string? search);
        Task<ServiceCatalogue?> GetByIdAsync(Guid id);
        Task<ServiceCatalogue?> GetByNameAsync(string name);
        Task<ServiceCatalogue?> CreateAsync(ServiceCatalogueArgument svc);
        Task<ServiceCatalogue?> UpdateAsync(Guid id, ServiceCatalogueArgument svc);
        Task DeleteAsync(Guid id);
    }
}
