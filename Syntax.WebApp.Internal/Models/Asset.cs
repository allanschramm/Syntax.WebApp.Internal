using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class Asset
    {
        [Key]public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Description { get; set; }
        public AssetClass AssetClass { get; set; }

    }
}
