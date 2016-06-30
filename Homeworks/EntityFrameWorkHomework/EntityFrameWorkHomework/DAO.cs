using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkHomework
{
    using System.Data.Entity.Core;

    public static class DAO
    {
      

        public static SoftUniEntities Context { get; set; }

        public static void Add(Employee employee)
        {
            Context.Employees.Add(employee);
            Context.SaveChanges();
        }

        public static Employee FindByKey(object key)
        {
            var employee = Context.Employees.Find(key);
            if (employee == null)
            {
                throw new ObjectNotFoundException("There is not such employee in the database");
            }
            
            return employee;
        }

        public static void Modify(Employee employee)
        {
            Context.SaveChanges();
        }

        public static void Delete(Employee employee)
        {
            Context.Employees.Remove(employee);
            Context.SaveChanges();
        }
    }
}
