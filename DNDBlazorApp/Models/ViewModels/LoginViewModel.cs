using System.ComponentModel.DataAnnotations;

namespace DNDBlazorApp.Models.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter User Name")]
    public string? Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
    public string? Password { get; set; }
}
