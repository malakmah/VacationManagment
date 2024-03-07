namespace VacationManagement.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
