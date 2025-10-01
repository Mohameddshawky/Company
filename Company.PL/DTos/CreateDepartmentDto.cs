using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Name is Required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Code must be a positive number")]
        public string Code { get; set; }
        [Required]

        public DateTime CreateAt { get; set; }

    }
}
