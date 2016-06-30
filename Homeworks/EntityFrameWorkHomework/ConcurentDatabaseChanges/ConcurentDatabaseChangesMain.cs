using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurentDatabaseChanges
{
    using EntityFrameWorkHomework;

    class ConcurentDatabaseChangesMain
    {
        static void Main(string[] args)
        {
            var context1 = new SoftUniEntities();
            var context2 = new SoftUniEntities();

            var entryToChange1 = context1.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            var entryToChange2 = context2.Employees.FirstOrDefault(e => e.LastName == "Nakov");


            entryToChange1.LastName = "Barbanakov";
            entryToChange2.LastName = "Nananakov";


            context1.SaveChanges();
            context2.SaveChanges();

            /* if concurrency mode is None, the last who make changes always wins.
            * if concurrency mode is Fixed, until the first context saves the changes, nobody can't make changes */

        }
    }
}
