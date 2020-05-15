using System;
using System.Xml.Serialization;

namespace BooksBasicSerializationLibrary.Models
{
    [XmlRoot("catalog", Namespace = "http://library.by/catalog",
        IsNullable = false)]
    public class Catalog
    {
        [XmlAttribute("date", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement("book", typeof(Book))]
        public Book[] Books { get; set; }
    }
}
