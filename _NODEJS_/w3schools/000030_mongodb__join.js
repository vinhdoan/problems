// orders
// [
//     { _id: 1, product_id: 154, status: 1 }
// ]

// products
// [
//     { _id: 154, name: 'Chocolate Heaven' },
//     { _id: 155, name: 'Tasty Lemons' },
//     { _id: 156, name: 'Vanilla Dreams' }
// ]

var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://127.0.0.1:27017/mydb";

MongoClient.connect(url, function(err, db) {
    if (err) throw err;
    db.collection('orders').aggregate([
        { $lookup:
          {
              from: 'products',
              localField: 'product_id',
              foreignField: '_id',
              as: 'orderdetails'
          }
        }
    ], function(err, res) {
        if (err) throw err;
        console.log(JSON.stringify(res));
        db.close();
    });
});
