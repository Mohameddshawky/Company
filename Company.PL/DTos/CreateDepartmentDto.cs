using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class CreateDepartmentDto
    {
        [Required]

        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]

        public DateTime CreateAt { get; set; }

    }
}
