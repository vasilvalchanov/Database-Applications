using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeSQLQuery
{
    using System.Diagnostics;

    using EntityFrameWorkHomework;

    class NativeSQLQueryMain
    {
        static void Main(string[] args)
        {
            var context = new SoftUniEntities();
            var empCount = context.Employees.Count();

            Console.WriteLine(empCount);

            var sw = new Stopwatch();
            sw.Start();
            PrintNamesWithLinqQuery();
            Console.WriteLine("Linq: {0}", sw.Elapsed);

            Console.WriteLine();
            sw.Restart();
            PrintNamesWithNativeQuery();
            Console.WriteLine("Native: {0}", sw.Elapsed);
        }

        public static void PrintNamesWithLinqQuery()
        {
            var context = new SoftUniEntities();
            var employeesNames =
                context.Employees.Where(e => e.Projects.Any(p => p.StartDate.Year == 2002)).Select(e => e.FirstName).Distinct();

            foreach (var name in employeesNames)
            {
                Console.WriteLine(name);
            }
        }

        public static void PrintNamesWithNativeQuery()
        {
            var queryAsString = @"SELECT e.FirstName FROM Employees e
                                JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
                                JOIN Projects p ON ep.ProjectID = p.ProjectID
                                WHERE YEAR(p.StartDate) = '2002'
                                GROUP BY e.FirstName
                                HAVING COUNT(p.ProjectID) > 0";

            var context = new SoftUniEntities();
            var employeesNames = context.Database.SqlQuery<string>(queryAsString);

            foreach (var name in employeesNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
