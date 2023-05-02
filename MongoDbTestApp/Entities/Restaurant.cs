using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbTestApp.Entities;

public class Restaurant
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    [BsonElement("restaurant_id")]
    public string RestaurantId { get; set; }

    public Address Address { get; set; }
    
    public List<GradeEntry> Grades { get; set; }
}