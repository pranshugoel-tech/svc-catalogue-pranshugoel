using System.ComponentModel.DataAnnotations.Schema;


namespace Service.Catalogue.Api.Models
{
    public class ServiceCatalogue
    {
        public Guid ID { get; set; } = new Guid();
        
        public string Name { get; set; } = string.Empty;
       
        public string OwnerTeam { get; set; } = string.Empty;
        
        public string? Tier { get; set; }
  
        public string? Lifecycle { get; set; }

        [Column(TypeName = "json")]
        public List<string> Endpoints { get; set; } = new();

        [Column(TypeName = "json")]
        public List<string> Tags { get; set; } = new();

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}