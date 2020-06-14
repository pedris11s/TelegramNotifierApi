
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TelegramBotNotifierApi.Persistence.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }

}
