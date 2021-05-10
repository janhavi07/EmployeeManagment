using EmployeeManagementML;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementBL
{
    public interface IEmployeeManager
    {
        Employee AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int empId);
        List<EmployeeResponse> GetAllEmployee();
    }
}
