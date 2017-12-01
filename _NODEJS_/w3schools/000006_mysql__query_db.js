var mysql = require('mysql');
var util = require('util');

var con = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "asdfasdf"
});

con.connect(function(err) {
    if (err) throw err;
    console.log("Connected!");
    var sql = "select user from mysql.user";
    con.query(sql, function (err, result) {
        if (err) throw err;
        console.log("Result: " + util.inspect(result, false, null));
    });
});
