using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_ComminityCares.Models
{

    public class Donor
    {
        
        public int Id { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }

    }
}
