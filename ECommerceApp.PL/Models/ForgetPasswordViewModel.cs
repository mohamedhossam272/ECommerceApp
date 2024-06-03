using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
