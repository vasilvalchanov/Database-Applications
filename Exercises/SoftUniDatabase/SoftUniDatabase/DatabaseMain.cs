using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDatabase
{
    class DatabaseMain
    {
        static void Main(string[] args)
        {
            var context = new SoftUniEntities();

            //var employeesNames = context.Employees.Where(e => e.Salary > 50000).Select(e => e.FirstName);

            //foreach (var employeeName in employeesNames)
            //{
            //    Console.WriteLine(employeeName);
            //}

            //var employees =
            //    context.Employees.Where(e => e.Department.Name == "Research and Development")
            //        .Select(e => new { e.FirstName, e.LastName, Department = e.Department.Name, e.Salary })
            //        .OrderBy(e => e.Salary)
            //        .ThenByDescending(e => e.FirstName);

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine("{0} {1} from {2} - ${3:F2}", employee.FirstName, employee.LastName, employee.Department, employee.Salary);
            //}

            //var address = new Address()
            //                  {
            //                      AddressText = "Vitoshka 15",
            //                      TownID = 4
            //                  };

            //Employee employee = null;

            //employee = context.Employees.First(e => e.LastName == "Nakov");
            //employee.Address = address;
            //context.SaveChanges();

            //var nakovAddress = context.Employees.First(e => e.LastName == "Nakov");
            //Console.WriteLine(nakovAddress.Address.AddressText);

            var projectToDelete = context.Projects.Find(2);



            foreach (var employee in projectToDelete.Employees)  // taka setvame zavisimostta na FK na null i chak sled tova moje da iztriem
            {
                employee.Projects = null;
            }
            
                context.Projects.Remove(projectToDelete);
                context.SaveChanges();
        }
    }
}
