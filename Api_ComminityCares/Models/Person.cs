using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_ComminityCares.Models
{
    public class Person
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public int status { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Ci { get; set; }
        public string PhoneNumber { get; set; }
    }
}
