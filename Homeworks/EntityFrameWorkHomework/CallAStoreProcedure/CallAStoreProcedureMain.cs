using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallAStoreProcedure
{
    using EntityFrameWorkHomework;

    class CallAStoreProcedureMain
    {
        static void Main(string[] args)
        {
            var projects = GetProjectsByEmployee("Ruth", "Ellerbrock");
            Console.WriteLine(projects);
        }

        public static string GetProjectsByEmployee(string firstName, string lastName)
        {
            var context = new SoftUniEntities();
            var result = context.usp_GetAllProjectsByEmployeeName(firstName, lastName);
            var sb = new StringBuilder();

            foreach (var project in result)
            {
                sb.AppendFormat("{0} - {1}, {2}", project.Name, project.Description, project.StartDate);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
