using BooksBasicSerializationLibrary.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BooksBasicSerializationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Catalog));

            FileStream stream = new FileStream("books.xml", FileMode.Open);
            Catalog catalog = xmlSerializer.Deserialize(stream) as Catalog;

            Console.WriteLine($"Catalog has {catalog.Books.Length} books");

            var serializer = new XmlSerializer(typeof(Catalog));
            var writeStream = new FileStream("books_new.xml", FileMode.Create);
            serializer.Serialize(writeStream, catalog);
            writeStream.Close();

            Console.WriteLine("Serialization has been finished");
        }
    }
}
