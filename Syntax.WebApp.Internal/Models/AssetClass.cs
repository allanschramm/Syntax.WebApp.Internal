using System.ComponentModel.DataAnnotations;

namespace Syntax.WebApp.Internal.Models
{
    public class AssetClass
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Name { get; set; }
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "O campo Ícone é obrigatório.")]
        public string? Icon { get; set; }
        public IFormFile? IconFile { get; set; }

    }
}
