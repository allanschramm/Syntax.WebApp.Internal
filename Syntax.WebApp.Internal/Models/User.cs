using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.WebApp.Internal.Models
{
    public class User
    {
        [Key] public string? Id { get; set; }
        [Column(TypeName = "nvarchar(30)")] public string? Name { get; set; }
        [Column(TypeName = "nvarchar(30)")] public string? LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")] public string? Email { get; set; }
        [Column(TypeName = "nvarchar(50)")] public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ReentryPassword { get; set; }
        [Column(TypeName = "nvarchar(10)")] public string? Role { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}