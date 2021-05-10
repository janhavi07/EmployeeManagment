using EmployeeManagementBL;
using EmployeeManagementML;
using EmployeeManagementRL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeManagementSytem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeManager manager;
        public EmployeeController(IEmployeeManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            try
            {
                var result = this.manager.AddEmployee(employee);
                if (result != null)
                {
                    return this.Ok(new { status = "True", message = "Employee Added Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "EmployeeNot  Added Successfully", data = result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{empId}")]
        public IActionResult DeleteEmployee(int empId)
        {
            var result = this.manager.DeleteEmployee(empId);
            if (result)
                return this.Ok(new { status = "True", message = "Employee Deleted Successfully", data = result });
            else
                return this.BadRequest(new { status = "False", message = "Employee Not Deleted ", data = result });
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                var result = this.manager.UpdateEmployee(employee);
                if (result)
                    return this.Ok(new { status = "True", message = "Employee Updated Successfully", data = result });
                else
                    return this.NotFound(new { status = "False", message = "Employee Not Updated ", data = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = "False", message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListOfEmployees()
        {
            try
            {
                var result = this.manager.GetAllEmployee();
                return this.Ok(new { status = "True", message = "Employee Listed Successfully", data = result });
            }
            catch(Exception e)
            {
                return this.NotFound(new { status = "False", message = e.Message });
            }
        }
    }
    
}
