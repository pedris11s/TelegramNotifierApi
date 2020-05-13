
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TelegramBotNotifierApi.Persistence.Models
{
    public class Channel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string HashId { get; set; }
        public string ChannelName { get; set; }
        public List<User> Users { get; set; }
    }
}
