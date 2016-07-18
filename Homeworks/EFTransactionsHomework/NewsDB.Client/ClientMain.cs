using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDB.Client
{
    using System.Data;
    using System.Data.Entity.Infrastructure;

    using NewsDB.Data;

    class ClientMain
    {
        static void Main()
        {
            var context = new NewsDBContext();
            var newContext = new NewsDBContext();
            var firstNews = context.News.FirstOrDefault();

            Console.WriteLine("Application started");
            Console.Write("Text from DB: ");
            Console.WriteLine(firstNews.NewsContent);
            Console.WriteLine("Enter the corrected text: ");


            firstNews.NewsContent = Console.ReadLine();

            try
            {
                context.SaveChanges();

                Console.WriteLine("Changes successfully saved in the DB.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var textFromDB = newContext.News.FirstOrDefault();
                Console.WriteLine("Conflict! Text from DB: {0}. Enter the corrected text:", textFromDB.NewsContent);
                Main();
            }
        }
    }
}
