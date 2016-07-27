using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMParser
{
    using System.Xml;

    class DOMParserMain
    {
        static void Main(string[] args)
        {
            var path = "../../../catalog.xml";

            //------------Problem 2------------------------------

            var albumNames = CatalogManager.ExtractAlbumNames(path);
            Console.WriteLine("Albums names:");
            Console.WriteLine(string.Join(Environment.NewLine, albumNames));
            Console.WriteLine();

            //------------Problem 3------------------------------

            var artists = CatalogManager.ExtractAllArtistsAlphabetically(path);
            Console.WriteLine("Artists names:");
            Console.WriteLine(string.Join(Environment.NewLine, artists));
            Console.WriteLine();

            //------------Problem 4------------------------------

            var artistWithAlbums = CatalogManager.ExtractArtistWithNumberOfAlbums(path);
            Console.WriteLine("Artists with number of albums:");

            foreach (var artist in artistWithAlbums)
            {
                Console.WriteLine("{0} - {1} albums", artist.Key, artist.Value);
            }

            Console.WriteLine();

            //------------Problem 5------------------------------

            var result = CatalogManager.ExtractArtistWithNumberOfAlbumsXpath(path);
            Console.WriteLine("Artists with number of albums:");

            foreach (var artist in result)
            {
                Console.WriteLine("{0} - {1} albums", artist.Key, artist.Value);
            }

            Console.WriteLine();

            //------------Problem 6------------------------------

            //CatalogManager.DeleteAlbumsMoreExpensiveThan(path, 20);

            //------------Problem 7------------------------------

            var albumsOlderThan = CatalogManager.ExtractAlbumsOlderThan(path, 5);

            foreach (var album in albumsOlderThan)
            {
                Console.WriteLine("Album name: {0}; Price: {1}", album.Key, album.Value);
            }

            Console.WriteLine();

            //------------Problem 8------------------------------

            var albumsOlderThanLinq = CatalogManager.ExtractAlbumsOlderThanLinq(path, 5);

            foreach (var album in albumsOlderThanLinq)
            {
                Console.WriteLine("Album name: {0}; Price: {1}", album.Key, album.Value);
            }

            Console.WriteLine();
        }
    }
}
