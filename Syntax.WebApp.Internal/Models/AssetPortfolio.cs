using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class AssetPortfolio
    {
        [Key]public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }

        public Portfolio Portfolio { get; set; }
        public Asset Asset { get; set; }

    }
}
