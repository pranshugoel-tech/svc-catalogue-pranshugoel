using CsvHelper.Configuration;

namespace Service.Catalogue.Api.DTOs;
public sealed class ServiceMap : ClassMap<ServiceCatalogueDto>
{
    public ServiceMap()
    {
        //Map(m => m.Id);
        Map(m => m.Name);
        Map(m => m.OwnerTeam);
        Map(m => m.Tier);
        Map(m => m.Lifecycle);
        Map(m => m.Endpoints).Convert(row =>
            row.Row.GetField("Endpoints").Split(';').ToList());
        Map(m => m.Tags).Convert(row =>
            row.Row.GetField("Tags").Split(';').ToList());
        //Map(m => m.CreatedAt);
        //Map(m => m.UpdatedAt);
    }
}
