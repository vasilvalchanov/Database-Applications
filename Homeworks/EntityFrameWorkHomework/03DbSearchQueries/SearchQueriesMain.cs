using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03DbSearchQueries
{
    using System.Globalization;

    using EntityFrameWorkHomework;
    using EntityFrameWorkHomework;

    class SearchQueriesMain
    {
        static void Main(string[] args)
        {


            /* Increase your Console Screen Buffer Size to see the whole output from:
             * Console / Properties / Layout / Screen Buffer Size Height : 4000
             */

            //var employeesWithProjects = GetEmployeesWithProjectsBetween(2001, 2003);
            //Console.WriteLine(employeesWithProjects);

            //PrintAddresses(10);

            //var employeeInfo = GetInfoAboutEmployeeById(1);
            //Console.WriteLine(employeeInfo);

            PrintDepartmentsWithMoreThan(5);
        }

        public static void PrintAddresses(int addressesCount)
        {
            var contex = new SoftUniEntities();
            var addresses =
                contex.Addresses.Select(
                    a => new { a.AddressText, Town = a.Town.Name, EmployeeCount = a.Employees.Count })
                    .OrderByDescending(a => a.EmployeeCount)
                    .ThenBy(a => a.Town)
                    .Take(addressesCount);

            foreach (var address in addresses)
            {
                Console.WriteLine("{0}, {1} - {2} employees", address.AddressText, address.Town, address.EmployeeCount);
            }



        }

        public static string GetEmployeesWithProjectsBetween(int startYear, int endYear)
        {
            var context = new SoftUniEntities();

            var employees =
                context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year >= startYear && p.StartDate.Year <= endYear))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Manager,
                    e.Projects
                }).OrderBy(e => e.FirstName);

            if (employees == null)
            {
                throw new ArgumentNullException("There aren't such employees!");
            }

            var result = new StringBuilder();
            foreach (var employee in employees)
            {
                result.AppendFormat("Employee Name: {0} {1}", employee.FirstName, employee.LastName);
                result.AppendLine();
                result.AppendFormat(
                    "Manager: {0}",
                    employee.Manager != null ? employee.Manager.FirstName + " " + employee.Manager.LastName : "No manager");
                result.AppendLine();


                result.AppendLine("Projects: ");

                foreach (var project in employee.Projects)
                {
                    result.AppendFormat("--- Project Name: {0}", project.Name);
                    result.AppendLine();
                    result.AppendFormat("--- Project Start Date: {0}", project.StartDate);
                    result.AppendLine();
                    result.AppendFormat(
                        "--- Project End Date: {0}",
                        project.EndDate != null ? project.EndDate.ToString() : "Not finished");
                    result.AppendLine();
                }

                result.AppendLine();


                result.AppendLine();

            }

            return result.ToString();
        }

        public static string GetInfoAboutEmployeeById(int Id)
        {
            var context = new SoftUniEntities();


            var employee =
                context.Employees.Where(e => e.EmployeeID == Id)
                    .Select(
                        e =>
                        new
                            {
                                e.FirstName,
                                e.LastName,
                                e.JobTitle,
                               Projects = e.Projects.Select(p => p.Name).OrderBy(p => p)
                            });
            var sb = new StringBuilder();

            foreach (var empl in employee)
            {
                sb.AppendFormat("Name: {0} {1}", empl.FirstName, empl.LastName);
                sb.AppendLine();
                sb.AppendFormat("JobTitle: {0}", empl.JobTitle);
                sb.AppendLine();
                sb.AppendFormat("Projects: ");
                sb.AppendLine();

                if (empl.Projects == null)
                {
                    sb.AppendFormat("--- No projects");
                    sb.AppendLine();
                }
                else
                {
                    foreach (var project in empl.Projects)
                    {
                        sb.AppendFormat("--- {0}", project);
                        sb.AppendLine();
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static void PrintDepartmentsWithMoreThan(int employeesCount)
        {
            var context = new SoftUniEntities();

            var departments =
                context.Departments.Where(d => d.Employees.Count > employeesCount)
                    .Select(
                        d =>
                        new
                            {
                                Department = d.Name,
                                Manager = d.Employee.LastName,
                                EmployeesCount = d.Employees.Count
                            })
                    .OrderBy(d => d.EmployeesCount);

            Console.WriteLine(departments.Count());

            foreach (var department in departments)
            {
                Console.WriteLine("--{0} - Manager: {1}, Employees: {2}", department.Department, department.Manager, department.EmployeesCount);
            }
        }
    }
}
