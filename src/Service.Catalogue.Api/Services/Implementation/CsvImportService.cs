using CsvHelper;
using Service.Catalogue.Api.DTOs;
using Service.Catalogue.Api.Models;
using Service.Catalogue.Api.Repositories;
using System.Globalization;

namespace Service.Catalogue.Api.Services;

public class CsvImportService : ICsvImportService
{
    private readonly IServiceRepository _repo;
    public CsvImportService(IServiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> ImportCsvAsync(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);

        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<ServiceMap>();
            var records = csv.GetRecords<ServiceCatalogueArgument>().ToList();

            int processed = 0;

            foreach (var record in records)
            {
                var data = await _repo.GetByNameAsync(record.Name);

                if (data != null) // if exists, update
                {
                    await _repo.UpdateAsync(data.Id, record);
                }
                else
                {
                    await _repo.CreateAsync(record);
                }

                processed++;
            }
            return processed;
        }
    }
}