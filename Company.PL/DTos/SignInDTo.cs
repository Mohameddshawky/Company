using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class SignInDTo
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public bool RemberMe { get; set; }
    }
}
