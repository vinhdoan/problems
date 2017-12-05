var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/mydb";

MongoClient.connect(url, function(err, db) {
  if (err) throw err;
    //Exclude the _id field from the result (fields default to true):
    db.collection("customers").find({}, { _id: false, name: true, adress: true }).toArray(function(err, result) {
    if (err) throw err;
        console.log(result);
        db.close();
    });
});
