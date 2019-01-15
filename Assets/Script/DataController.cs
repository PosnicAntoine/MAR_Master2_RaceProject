using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private GameData DataGame;
    string filePath; 
    void Start()
    {
        filePath = Application.dataPath + "/Resources/data/data.json";
        LoadData();
    }

    public Race GetBestRace(){
        return DataGame.bestRace;
    }

    public float GetBestScore(){
        float ti = DataGame.bestRace.Trajectory[0].x;
        float tf = DataGame.bestRace.Trajectory[DataGame.bestRace.Trajectory.Count-1].x;

        return tf -ti;
    }

    public Race GetLastRace(){
        return DataGame.lastRace;
    }

    void LoadData(){
        
        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath); 
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            DataGame = JsonUtility.FromJson<GameData>(dataAsJson);
            if(DataGame.bestRace == null){
                DataGame.bestRace = new Race();
            }
            if(DataGame.lastRace == null) {
                DataGame.lastRace = new Race();
            }
        }
        else
        {
            Debug.Log("Cannot load game data!");
            DataGame = new GameData();
        }
    }

    public void SubmitNewBestRace(Race race){
        float ti = race.Trajectory[0].x;
        float tf = race.Trajectory[race.Trajectory.Count-1].x;
        if (GetBestScore() > (tf-ti)){
            SaveNewBestRace(race);
        }
    }

    void SaveNewBestRace(Race race){
        DataGame.bestRace = race;
    }

    public void SaveLastRace(Race race){
        DataGame.lastRace = race;
    }
    void CreateDataFile(){
        string json = JsonUtility.ToJson(DataGame);
        using (FileStream fs = new FileStream(filePath, FileMode.Create)){
            using (StreamWriter writer = new StreamWriter(fs)){
                writer.Write(json);
                writer.Close();
                writer.Dispose();
            }
            fs.Close();
            fs.Dispose();
        }
    }

    public void UpdateDataFile(){
        string json = JsonUtility.ToJson(DataGame);
        File.WriteAllText(filePath, json);
    }

}
