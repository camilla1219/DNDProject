using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DNDBlazorApp.Models.Entities;

[Table("user_account")]
public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("password")]
    [MaxLength(100)]
    public string? Password { get; set; }

    [Column("role")]
    [MaxLength(20)]
    public string? Role { get; set; }
}
