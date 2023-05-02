using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDbTestApp.Entities;

const string connectionString = "mongodb://localhost:27017";

var client = new MongoClient(connectionString);

var restaurantCollection = client.GetDatabase("mongodb-dotnet-test").GetCollection<Restaurant>("restaurants");

var restaurant = new Restaurant
{
    Name = "Nick's Pizza",
    RestaurantId = Guid.NewGuid().ToString(),
    Address = new Address
    {
        Building = "Fancy",
        Coordinates = new[] { 1322.234, 23432.3453 },
        Street = "Funny",
    }
};

// await restaurantCollection.InsertOneAsync(restaurant); // async
// restaurantCollection.InsertOne(restaurant); // sync

var searchRestaurant = await restaurantCollection.AsQueryable().Where(r => r.Name == "Nick's Pizza").FirstOrDefaultAsync();

var searchRestaurants = await restaurantCollection.AsQueryable().Where(r => r.Name == "Nick's Pizza").ToListAsync();

Console.WriteLine(searchRestaurant.Address.Id);

// Update 
const string oldValue = "Bagels N Buns";
const string newValue = "2 Bagels 2 Buns";
var filter1 = Builders<Restaurant>.Filter
    .Eq(r => r.Name, oldValue);
var update = Builders<Restaurant>.Update
    .Set(r => r.Name, newValue); 
await restaurantCollection.UpdateOneAsync(filter1, update);


//Replace
var filter2 = Builders<Restaurant>.Filter
    .Eq(r => r.Name, "Nick's Pizza");
// Find ID of first pizza restaurant
var oldPizzaRestaurant = restaurantCollection.Find(filter2).First();
var oldId = oldPizzaRestaurant.Id;
Restaurant newPizzaRestaurant = new()
{
    Id = oldId,
    Name = "Mongo's Pizza",
};
await restaurantCollection.ReplaceOneAsync(filter2, newPizzaRestaurant);

//Delete
var filter3 = Builders<Restaurant>.Filter
    .Eq(r => r.Name, "Ready Penny Inn");
await restaurantCollection.DeleteOneAsync(filter3);