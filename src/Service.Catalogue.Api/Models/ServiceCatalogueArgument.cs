using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Service.Catalogue.Api.Models
{
    public class ServiceCatalogueArgument
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string OwnerTeam { get; set; } = string.Empty;

        [Required]
        public string? Tier { get; set; } // gold|silver|bronze

        [Required]
        public string? Lifecycle { get; set; } // production|preprod|dev|deprecated

        [Column(TypeName = "json")]
        public List<string> Endpoints { get; set; } = new();

        [Column(TypeName = "json")]
        public List<string> Tags { get; set; } = new();
    }
}