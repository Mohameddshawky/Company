using System.ComponentModel.DataAnnotations;
namespace Company.PL.DTos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email can not be Empty")]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
    }
}
