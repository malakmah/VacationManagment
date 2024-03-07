namespace VacationManagement.Models
{
    public class RegisterLogin 
    {
        public string?  Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password don`t match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword{ get; set; }

        [DataType(DataType.MultilineText)]  
        public string? Address { get; set; }

  

    



    }
}
