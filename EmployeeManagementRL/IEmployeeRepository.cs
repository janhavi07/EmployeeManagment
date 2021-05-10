using EmployeeManagementML;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementRL
{
    public interface IEmployeeRepository
    {
        Employee AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int empId);
        List<EmployeeResponse> GetAllEmployee();
        bool Login(string email);
    }
}
