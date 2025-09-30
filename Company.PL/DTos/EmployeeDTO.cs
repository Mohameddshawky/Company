using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTos
{
    public class EmployeeDTO
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Range(22,60,ErrorMessage ="Age must Be Between 22 and 60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [RegularExpression( pattern: @"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="" +
            "Address must be like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date Of Creation")]

        public DateTime CreateAt { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }


    }
}
