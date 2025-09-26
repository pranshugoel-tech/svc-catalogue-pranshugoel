namespace Service.Catalogue.Api.Services
{
    public interface ICsvImportService
    {
        Task<int> ImportCsvAsync(Stream csvStream);
    }
}
