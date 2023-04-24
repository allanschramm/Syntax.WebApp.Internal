using System.ComponentModel.DataAnnotations;

namespace Syntax.WebApp.Internal.Models
{
    public class Asset
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Description { get; set; }
        #region ALTERAÇÃO DE ABORGAGEM
        //public AssetClass AssetClass { get; set; }
        #endregion
        public int IdAssetClass { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Grade { get; set; }
        public virtual AssetClass? AssetClassNavigation { get; set; }

    }
}
