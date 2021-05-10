using EmployeeManagementML;
using EmployeeManagementRL;
using System;
using System.Collections.Generic;

namespace EmployeeManagementBL
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeRepository repository;

        public EmployeeManager(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public Employee AddEmployee(Employee employee)
        {
            try
            {
                return this.repository.AddEmployee(employee);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                return this.repository.DeleteEmployee(empId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EmployeeResponse> GetAllEmployee()
        {
            try { 

                return this.repository.GetAllEmployee();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
             }
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                return this.repository.UpdateEmployee(employee);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
