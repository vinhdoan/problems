var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/mydb";

MongoClient.connect(url, function(err, db) {
    if (err) throw err;
    //Sort the result by name (-1 for descending, 1 for ascending):
    var sort = { name: 1 };
    db.collection("customers").find().sort(sort).toArray(function(err, result) {
        if (err) throw err;
        console.log(result);
        db.close();
    });
});
