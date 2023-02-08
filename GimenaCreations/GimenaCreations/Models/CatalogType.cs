using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models
{
    [Display(Name = "Catalog type")]
    public class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
