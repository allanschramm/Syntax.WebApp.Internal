using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.Models
{
    public class User
    {

        [Key] public int Id { get; set; }
        [Column(TypeName = "nvarchar(30)")] public string? Name { get; set; }
        [Column(TypeName = "nvarchar(50)")] public string? Email { get; set; }
        [Column(TypeName = "nvarchar(50)")] public string? Password { get; set; }
        [Column(TypeName = "nvarchar(10)")] public string? Role { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastAccessDate { get; set;}
        public bool IsEmailConfirmed { get; set;}
        
    }
}
