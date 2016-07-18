using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AdsSystem
{
    using System.Diagnostics;

    class AdsSystemMain
    {
        static void Main(string[] args)
        {
            // --------------Problem 1 ---------------

            var context = new AdsEntities();
            //var ads = context.Ads;

            //foreach (var ad in ads)
            //{
            //    Console.WriteLine("Title: {0}", ad.Title);
            //    Console.WriteLine("Status: {0}", ad.AdStatus.Status);
            //    Console.WriteLine("Category: {0}", ad.Category != null ? ad.Category.Name : "not specified");
            //    Console.WriteLine("Town: {0}", ad.Town != null ? ad.Town.Name : "not specified");
            //    Console.WriteLine("User: {0}", ad.AspNetUser.UserName);
            //    Console.WriteLine();
            //}

            //var adsWithInclude = context.Ads.Include(a => a.AdStatus)
            //    .Include(a => a.Category)
            //    .Include(a => a.Town)
            //    .Include(a => a.AspNetUser);

            //foreach (var ad in adsWithInclude)
            //{
            //    Console.WriteLine("Title: {0}", ad.Title);
            //    Console.WriteLine("Status: {0}", ad.AdStatus.Status);
            //    Console.WriteLine("Category: {0}", ad.Category != null ? ad.Category.Name : "not specified");
            //    Console.WriteLine("Town: {0}", ad.Town != null ? ad.Town.Name : "not specified");
            //    Console.WriteLine("User: {0}", ad.AspNetUser.UserName);
            //    Console.WriteLine();
            //}

            //+--------------------------+---------------+-----------------+
            //|                          | No Include(…) | With Include(…) |
            //+--------------------------+---------------+-----------------+
            //| Number of SQL statements |             28|                1|
            //+--------------------------+---------------+-----------------+


            // --------------Problem 2 ---------------


            var sw = new Stopwatch();
            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS DBCC FREEPROCCACHE");
            //sw.Start();
            //var adsSlow =
            //    context.Ads.ToList()
            //        .Where(a => a.AdStatus.Status == "Published")
            //        .Select(a => new { Title = a.Title, Category = a.Category, Town = a.Town, Date = a.Date })
            //        .ToList()
            //        .OrderBy(a => a.Date);

            //Console.WriteLine(sw.ElapsedMilliseconds);

            //context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS DBCC FREEPROCCACHE");
            //sw.Restart();
            //var adsFast =
            //    context.Ads
            //        .Where(a => a.AdStatus.Status == "Published")
            //        .Select(a => new { Title = a.Title, Category = a.Category, Town = a.Town, Date = a.Date })
            //        .OrderBy(a => a.Date).ToList();

            //Console.WriteLine(sw.ElapsedMilliseconds);


            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+
            // |                    | Run 1 | Run 2 | Run 3 | Run 4 | Run 5 | Run 6 | Run 7 | Run 8 | Run 9 | Run 10 | Average Time |
            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+
            // | Non-optimized (ms) |  191  | 195  |  248  |  163  |  197  |  230  | 164   |  177  |   175 |  183   |       192.3   |
            // | Optimized (ms)     |   77  |  71  |   118 |  78   |   81  |   96  |  78   |   83  |   78  |    83  |        84.3   |
            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+


            // --------------Problem 3 ---------------


            sw.Restart();
            context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS DBCC FREEPROCCACHE");
            var notOptimized = context.Ads;
            foreach (var ad in notOptimized)
            {
                Console.WriteLine(ad.Title);
            }

            Console.WriteLine();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS DBCC FREEPROCCACHE");
            var optimized = context.Ads.Select(a => a.Title);
            foreach (var ad in optimized)
            {
                Console.WriteLine(ad);
            }

            Console.WriteLine();
            Console.WriteLine(sw.ElapsedMilliseconds);


            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+
            // |                    | Run 1 | Run 2 | Run 3 | Run 4 | Run 5 | Run 6 | Run 7 | Run 8 | Run 9 | Run 10 | Average Time |
            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+
            // | Non-optimized (ms) | 386   | 379   |  502  |  491  |   516 |  348  |  418  |  330  |  490  |   423  |  428.3       |
            // | Optimized (ms)     |  91   |  121  |  105  | 103   |  78   |    85 |   67  |   78  |  94   |     73 |    89.5      |
            // +--------------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+--------------+

        }
    }
}
