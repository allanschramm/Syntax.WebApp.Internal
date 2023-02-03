using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class AssetClass
    {
        [Key]public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
