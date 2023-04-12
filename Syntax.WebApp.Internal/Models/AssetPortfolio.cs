using System.ComponentModel.DataAnnotations;

namespace Syntax.WebApp.Internal.Models
{
    public enum EventTypeAssetPortfolio
    {
        Compra,
        Venda        
    }
    public class AssetPortfolio
    {
        [Key] public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime Date { get; set; }
        public EventTypeAssetPortfolio Type { get; set; }
        #region ALTERAÇÃO DE ABORGAGEM
        //public Portfolio Portfolio { get; set; }
        //public Asset Asset { get; set; }
        #endregion
        public int IdPortfolio { get; set; }
        public int IdAsset { get; set; }
        public virtual Portfolio? PortFolioNavigation { get; set; }
        public virtual Asset? AssetNavigation { get; set; }



    }
}
