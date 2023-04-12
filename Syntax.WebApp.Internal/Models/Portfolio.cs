using System.ComponentModel.DataAnnotations;

namespace Syntax.WebApp.Internal.Models
{
    public class Portfolio
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        #region ALTERAÇÃO DE ABORDAGEM
        //public User? User { get; set; }
        #endregion
        public int IdUser { get; set; }
        public virtual User? UserNavigation { get; set; }


    }
}
