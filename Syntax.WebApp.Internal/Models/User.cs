using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.WebApp.Internal.Models
{
    public class User
    {
        [Key] public string? Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Column(TypeName = "nvarchar(30)")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório")]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Confirmação de Password é obrigatório")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não correspondem.")]
        public string? ReentryPassword { get; set; }
        [Column(TypeName = "nvarchar(10)")] public string? Role { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string FullName => $"{LastName}, {Name}";

    }
}