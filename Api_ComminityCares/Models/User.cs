using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Api_ComminityCares.Models
{
    public class User
    {
        
        public int Id { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
