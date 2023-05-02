using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbTestApp.Entities;

public class Address
{
    public ObjectId Id { get; set; }
    
    public string Building { get; set; }
    
    [BsonElement("coord")]
    public double[] Coordinates { get; set; }
    public string Street { get; set; }

    // Add this constructor to insert id in sub-documents if you need this
    public Address()
    {
        Id = ObjectId.GenerateNewId();
    }
}