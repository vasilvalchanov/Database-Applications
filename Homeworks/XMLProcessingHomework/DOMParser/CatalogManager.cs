using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMParser
{
    using System.Xml;
    using System.Xml.Linq;

    public static class CatalogManager
    {
        public static ICollection<string> ExtractAlbumNames(string path)
        {
            var albumNames = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes.Item(i).Name == "name")
                    {
                        albumNames.Add(node["name"].InnerText);
                    }
                }
            }

            return albumNames;
        }

        public static ICollection<string> ExtractAllArtistsAlphabetically(string path)
        {
            SortedSet<string> artists = new SortedSet<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes.Item(i).Name == "artist")
                    {
                        artists.Add(node["artist"].InnerText);
                    }
                }
            }

            return artists;
        }

        public static IDictionary<string, int> ExtractArtistWithNumberOfAlbums(string path)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes.Item(i).Name == "artist")
                    {
                        if (!data.ContainsKey(node["artist"].InnerText))
                        {
                            data.Add(node["artist"].InnerText, 1);
                        }
                        else
                        {
                            data[node["artist"].InnerText]++;
                        }
                    }
                }
            }

            return data;
        }

        public static IDictionary<string, int> ExtractArtistWithNumberOfAlbumsXpath(string path)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;

            var artists = rootNode.SelectNodes("/albums/album/artist");

            foreach (XmlNode node in artists)
            {
                if (!data.ContainsKey(node.InnerText))
                {
                    data.Add(node.InnerText, 1);
                }
                else
                {
                    data[node.InnerText]++;
                }
            }

            return data;
        }

        public static void DeleteAlbumsMoreExpensiveThan(string path, decimal price)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;
            var albums = rootNode.SelectNodes("/albums/album");

            foreach (XmlNode node in albums)
            {
                var albumPrice = decimal.Parse(node["price"].InnerText);

                if (albumPrice > price)
                {
                    rootNode.RemoveChild(node);
                }
            }

            doc.Save("../../../cheap-albums-catalog.xml");
        }

        public static IDictionary<string, decimal> ExtractAlbumsOlderThan(string path, int years)
        {
            IDictionary<string, decimal> resultAlbums = new Dictionary<string, decimal>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode rootNode = doc.DocumentElement;
            var albums = rootNode.SelectNodes("/albums/album");

            foreach (XmlNode album in albums)
            {
                var albumDate = int.Parse(album["year"].InnerText);
                var price = decimal.Parse(album["price"].InnerText);
                var name = album["name"].InnerText;

                if (DateTime.Now.Year - years > albumDate)
                {
                    resultAlbums.Add(name, price);
                }
            }

            return resultAlbums;
        }

        public static IDictionary<string, decimal> ExtractAlbumsOlderThanLinq(string path, int years)
        {
            IDictionary<string, decimal> resultAlbums = new Dictionary<string, decimal>();

            XDocument xDocument = XDocument.Load(path);

            var result =
                xDocument.Descendants("album")
                    .Where(a => int.Parse(a.Element("year").Value) < DateTime.Now.Year - years)
                    .Select(al => new { albumName = al.Element("name").Value, albumPrice = al.Element("price").Value });

            foreach (var album in result)
            {
                resultAlbums.Add(album.albumName, decimal.Parse(album.albumPrice));
            }

            return resultAlbums;

        }
    }
}
