using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data.SqlClient;
using System.Data;

public class CreateDatabase
{
    string str;
    string dbName = Application.dataPath + "localData.";
    SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");
    private string dbPath = Application.dataPath + "/StreamingAssets/db.bytes";
    string logFileName = Application.dataPath + "MydatabaseLog.ldf";


    public CreateDatabase()
    {
        str = "CREATE DATABASE MyDatabase ON PRIMARY " +
        "(NAME = MyDatabase, " +
        "FILENAME = '" + dbPath + "', " +
        "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
        "LOG ON (NAME = MyDatabase_Log, " +
        "FILENAME = 'C:\\MydatabaseLog.ldf', " +
        "SIZE = 1MB, " +
        "MAXSIZE = 50MB, " +
        "FILEGROWTH = 10%)";

        SqlCommand myCommand = new SqlCommand(str, myConn);
        try
        {
            myCommand.BeginExecuteNonQuery();
            myCommand.ExecuteNonQuery();
            Debug.Log("DataBase is Created Successfully");
        }
        catch (SqlException ex)
        {
            Debug.Log(ex.ToString());
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }
    }
}