using Mono.Data.Sqlite; 
using System.Data; 
using System;
using UnityEngine;

public class ManageDatabase
{

    static string uri = "URI=file:" + Application.dataPath + "/Resources/Database/mar_database.db";
    IDbConnection dbconn;  

    void Connection(){
        if (dbconn == null) {
            dbconn = (IDbConnection) new SqliteConnection(uri);
            dbconn.Open();
        }
    }


    void Close(){
        if (dbconn != null){
            dbconn.Close();
            dbconn = null;
        }
    }

    public void getRaceTest(){
        //Race race = new Race();

        Connection();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM RACE";

        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            long id = long.Parse(reader.GetValue(0).ToString());
            string name = reader.GetString(1);
            float time = reader.GetFloat(2);
            float x = reader.GetFloat(3);
            float y = reader.GetFloat(4);
            float z = reader.GetFloat(5);
        
            Debug.Log("id: "+ id + " name: " + name + " time: " + time + " x: " + x + " y: " + y + " z: " + z);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;

        Close();
    }

    public void addRace(Race race){
        Connection();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string insert ="";
        foreach(Vector4 position in race.Trajectory){
            insert += "INSERT INTO RACE VALUES (\'" + race.Id + "\',\'" + race.Name + "\',\'" + 
                position.x + "\',\'" + position.y + "\',\'" + position.z + "\',\'" + position.w +"\');";
        }
        dbcmd.CommandText = insert;
        dbcmd.ExecuteNonQuery();
        Close();
    }


}
