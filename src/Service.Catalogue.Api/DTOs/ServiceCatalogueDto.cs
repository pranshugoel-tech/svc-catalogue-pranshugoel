using System;
using System.Collections.Generic;

namespace Service.Catalogue.Api.DTOs;
public class ServiceCatalogueDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // unique
    public string OwnerTeam { get; set; } = string.Empty;
    public string Tier { get; set; } = "bronze"; // gold | silver | bronze
    public string Lifecycle { get; set; } = "dev"; // production | preprod | dev | deprecated
    public List<string> Endpoints { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
