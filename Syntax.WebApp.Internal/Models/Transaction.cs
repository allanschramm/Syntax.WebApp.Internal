using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.WebApp.Internal.Models
{
    public enum EventTypeTransaction
    {
        Despesas,
        Renda
    }
    public class Transaction
    {
        [Key] public int Id { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public EventTypeTransaction Type { get; set; }
        #region ALTERAÇÃO DE ABORDAGEM
        //public User? User { get; set; }
        //public TransactionClass TransactionClass { get; set; }
        #endregion
        public int IdUser { get; set; }
        public int IdTransactionClass { get; set; }
        public virtual User? UserNavigation { get; set; }
        public virtual TransactionClass? TransactionClassNavigation { get; set; }


    }
}