using EmployeeManagementML;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagementRL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private OracleConnection oracleConnection;
        private IConfiguration configuration;
        
        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void connection()
        {
            string constr = this.configuration.GetConnectionString("UserDbConnection");
            oracleConnection = new OracleConnection(constr);

        }
        public Employee AddEmployee(Employee employee)
        {
            try
            {
                connection();
                string password = Encryptdata(employee.Password);
                OracleCommand com = new OracleCommand("sp_AddEmployee", this.oracleConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@FisrtName", employee.FirstName);
                com.Parameters.Add("@LastName", employee.LastName);
                com.Parameters.Add("@Email", employee.EmailId);
                com.Parameters.Add("@Password", password);
                com.Parameters.Add("@Dep_Id", employee.DepartmentId);
                com.Parameters.Add("@Salary", employee.Salary);
                oracleConnection.Open();

                com.ExecuteNonQuery();
                oracleConnection.Close();
                return employee;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                oracleConnection.Close();
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                connection();
                OracleCommand com = new OracleCommand("sp_DeleteEmployee", this.oracleConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@emp_id", empId);
                oracleConnection.Open();
                int isDelete = com.ExecuteNonQuery();

                if (isDelete != 0)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<EmployeeResponse> GetAllEmployee()
        {
            try
            {
                connection();
                List<EmployeeResponse> EmpList = new List<EmployeeResponse>();
                OracleCommand com = new OracleCommand("get_all_employees", this.oracleConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter adapter = new OracleDataAdapter(com);
                DataTable table = new DataTable();
                oracleConnection.Open();
                adapter.Fill(table);
                oracleConnection.Close();
                foreach (DataRow row in table.Rows)
                {
                    EmpList.Add(
                        new EmployeeResponse
                        {
                            EmpId = Convert.ToInt32(row["EMPID"]),
                            FirstName = Convert.ToString(row["FIRSTNAME"]),
                            LastName = Convert.ToString(row["LASTNAME"]),
                            EmailId = Convert.ToString(row["EMAIL"]),
                            Department = Convert.ToString(row["DEPARTMENTS"]),
                            Salary = Convert.ToInt32(row["SALARY"])
                        }
                        );
                }
                return EmpList;
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
                connection();
                OracleCommand com = new OracleCommand("sp_UpdateEmployee", this.oracleConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@EmpId", employee.EmpId);
                com.Parameters.Add("@FisrtName", employee.FirstName);
                com.Parameters.Add("@LastName", employee.LastName);
                com.Parameters.Add("@emailId", employee.EmailId);
                com.Parameters.Add("@password", employee.Password);
                com.Parameters.Add("@departmentId", employee.DepartmentId);
                com.Parameters.Add("@salary", employee.Salary);
                oracleConnection.Open();
                var result = com.ExecuteNonQuery();
                oracleConnection.Close();
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
                
        }
        public bool Login(string email)
        {
            // string password = Encryptdata(employee.Password);
            OracleCommand com = new OracleCommand("sp_Login", this.oracleConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("Email",email);
           


            return true;
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        
    }

}
