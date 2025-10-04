using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Username can not be null")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Firstname can not be null")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname can not be null")]

        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string confirmPassword { get; set; }
        public bool IsAgreed { get; set; }
    }
}
