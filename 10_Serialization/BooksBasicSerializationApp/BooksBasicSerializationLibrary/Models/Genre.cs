using System.Xml.Serialization;

namespace BooksBasicSerializationLibrary.Models
{
    public enum Genre
    {
        Computer,
        Fantasy,
        Romance,
        Horror,
        [XmlEnum(Name = "Science Fiction")]
        ScienceFiction,
    }
}
