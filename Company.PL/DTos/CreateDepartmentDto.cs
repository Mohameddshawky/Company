using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Name is Required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }
        [Required]

        public DateTime CreateAt { get; set; }

    }
}
