using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class ResetPasswordDto
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
