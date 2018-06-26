(function () {
    "use strict";

    // 定义一个空的Sqlite数据库
    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    var db = null;

    function onDeviceReady() {

        // 创建一个 Sqlite 数据库实例 
        db = window.sqlitePlugin.openDatabase({ name: 'demo.db', location: 'default' }, function (db) {
            // 用户可以在这里创建或者打开数据库表
        }, function (error) {
            console.log('数据库创建失败: ' + JSON.stringify(error));
        });

        var createDT01 = document.getElementById('createDT01');
        createDT01.addEventListener("click", createTableByTransactionAPI);
        var getDT01 = document.getElementById('getDT01');
        getDT01.addEventListener("click", getDataOfDemoTable01);
    };

    // 创建一个数据库表 DemoTable01，并向其中添加一些数据（使用标准的事务处理 API）
    function createTableByTransactionAPI() {
        db.transaction(function (tx) {
            tx.executeSql('CREATE TABLE IF NOT EXISTS DemoTable01 (name, score)');
            tx.executeSql('INSERT INTO DemoTable01 VALUES (?,?)', ['Alice', 101]);
            tx.executeSql('INSERT INTO DemoTable01 VALUES (?,?)', ['Betty', 202]);
        }, function (error) {
            document.getElementById('noticeContent').innerHTML = error.message;
        }, function () {
            document.getElementById('noticeContent').innerHTML = '数据库表 DemoTable01 创建成功';
        });
    }

    // 获取数据（使用标准的事务处理 API）
    function getDataOfDemoTable01() {
        db.transaction(function (tx) {
            tx.executeSql('SELECT name, score FROM DemoTable01', [], function (tx, rs) {
                document.getElementById('noticeContent').innerHTML = '当前数据条数：' + rs.rows.length;
                var resultString = "";
                for (var i = 0; i < rs.rows.length; i++) {
                    resultString = resultString + rs.rows.item(i).name + " | " + rs.rows.item(i).score + "<br/>";
                }
                document.getElementById('apiContent').innerHTML = resultString;

            }, function (tx, error) {
                document.getElementById('noticeContent').innerHTML = error.message;
            });
        });
    }

    // 创建一个数据库表 DemoTable02，并向其中添加一些数据（使用标准的Sql批处理 API）
    function createTableBySqlAPI() {
        db.sqlBatch([
            'CREATE TABLE IF NOT EXISTS DemoTable02 (name, score)',
            ['INSERT INTO DemoTable02 VALUES (?,?)', ['Alice', 101]],
            ['INSERT INTO DemoTable02 VALUES (?,?)', ['Betty', 202]],
        ], function () {
            console.log('数据库表 DemoTable02 创建成功');
        }, function (error) {
            console.log('SQL 批处理错误: ' + error.message);
        });
    }

    // 获取数据（使用标准的事务处理 API）
    function getDataOfDemoTable02() {
        db.executeSql('SELECT name, score FROM DemoTable01', [], function (rs) {
            document.getElementById('noticeContent').innerHTML = '当前数据条数：' + rs.rows.length;
            var resultString = "";
            for (var i = 0; i < rs.rows.length; i++) {
                resultString = resultString + rs.rows.item(i).name + " | " + rs.rows.item(i).score + "<br/>";
            }
            document.getElementById('apiContent').innerHTML = resultString;

        }, function (error) {
            document.getElementById('noticeContent').innerHTML = error.message;
        });
    }

    // 向数据库表添加纪录
    function addItem(name,score) {
        db.transaction(function (tx) {
            var query = "INSERT INTO DemoTable02 (name, score) VALUES (?,?)";
            tx.executeSql(query, [name,score], function (tx, res) {
            },function (tx, error) {
                console.log('INSERT error: ' + error.message);
            });
        }, function (error) {
            console.log('transaction error: ' + error.message);
        }, function () {
            console.log('transaction ok');
        });
    }

    // 根据指定的字段值提取记录
    function getData(name) {
        db.transaction(function (tx) {
            var query = "SELECT name,score FROM DemoTable02 WHERE name = ?";
            tx.executeSql(query, [name], function (tx, resultSet) {

                for (var x = 0; x < resultSet.rows.length; x++) {
                    // 处理数据结果
                }
            },function (tx, error) {
                console.log('SELECT error: ' + error.message);
            });
        }, function (error) {
            console.log('transaction error: ' + error.message);
        }, function () {
            console.log('transaction ok');
        });
    }

    // 根据指定的字段值删除记录
    function removeItem(name) {
        db.transaction(function (tx) {
            var query = "DELETE FROM DemoTable02 WHERE acctNo = ?";
            tx.executeSql(query, [name], function (tx, res) {
                // 处理数据结果
            },function (tx, error) {
                console.log('DELETE error: ' + error.message);
            });
        }, function (error) {
            console.log('transaction error: ' + error.message);
        }, function () {
            console.log('transaction ok');
        });
    }

    // 根据指定的字段值更新记录
    function updateItem(name, score) {
        db.transaction(function (tx) {

            var query = "UPDATE DemoTable02 SET score = ? WHERE name = ?";

            tx.executeSql(query, [score, name], function (tx, res) {
                // 处理数据结果
            },
            function (tx, error) {
                console.log('UPDATE error: ' + error.message);
            });
        }, function (error) {
            console.log('transaction error: ' + error.message);
        }, function () {
            console.log('transaction ok');
        });
    }

    // 关闭数据库
    function closeDB() {
        db.close(function () {
            console.log("DB closed!");
        }, function (error) {
            console.log("Error closing DB:" + error.message);
        });
    }
})();