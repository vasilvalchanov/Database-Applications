using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkHomework
{
    class DbContextMain
    {
        static void Main(string[] args)
        {
            DAO.Context = new SoftUniEntities();

            //var employee = new Employee()
            //                   {
            //                       FirstName = "Temelko",
            //                       LastName = "Ivanov",
            //                       JobTitle = "Tool Designer",
            //                       DepartmentID = DAO.Context.Departments.Where(d => d.Name == "Tool Design").Select(d => d.DepartmentID).FirstOrDefault(),
            //                       HireDate = DateTime.Now,
            //                       Salary = 33000                                 
            //                   };

            //DAO.Add(employee);

            //var temelkosPrimaryKey =
            //    DAO.Context.Employees.Where(e => e.FirstName == "Temelko").Select(e => e.EmployeeID).FirstOrDefault();

            //Console.WriteLine("Temelkos Id is: {0}", temelkosPrimaryKey);

            //var employeeToModify = DAO.Context.Employees.FirstOrDefault(e => e.FirstName == "Temelko");
            //employeeToModify.FirstName = "Parlapun";
            //DAO.Modify(employeeToModify);

            var employeeToDelete = DAO.Context.Employees.FirstOrDefault(e => e.FirstName == "Parlapun");
            DAO.Delete(employeeToDelete);
            
        }
    }
}
