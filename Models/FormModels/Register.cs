using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Register
    {
        [Display(Name = "First Name:")]
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be 3 characters or longer!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be 3 characters or longer!")]
        public string LastName { get; set; }

        [Display(Name = "Email:")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

    }
}