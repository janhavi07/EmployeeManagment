using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementML
{
    public class Employee
    {
        public int EmpId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Add is required.")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string EmailId{ get; set; }

        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Department Id is required.")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public float Salary { get; set; }

    }
}
