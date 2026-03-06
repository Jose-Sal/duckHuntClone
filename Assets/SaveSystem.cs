using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    private static string folderPath = Application.persistentDataPath + "/Saves";
    private static string path = folderPath + "/Saves.txt";



    public static void Init()
    {
        //check if the Saves folder exists in the persistent data path, if it doesn't exist create it
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static void SaveManager()
    {

        #region
        //BinaryFormatter formatter = new BinaryFormatter();

        //FileStream stream = new FileStream(path, FileMode.Create);//maybe put this after the if statement that checks if the Saves folder exists

        ////check if the Saves folder exists in the persistent data path, if it doesn't exist create it
        ////if (!Directory.Exists(path))
        ////{
        ////    Directory.CreateDirectory(path);

        ////}


        ////all appropriate information about the player that we want to save and load should be stored in the DataToSave class, this is the class that will be serialized and deserialized when saving and loading the game
        //DataToSave data = new DataToSave();

        //formatter.Serialize(stream, data);
        //stream.Close();
        #endregion
        //store data for saving
        DataToSave data = new DataToSave();
        
        //convert the savedObject to a JSON string
        string json = JsonUtility.ToJson(data);
        //save the JSON string to a file
        File.WriteAllText(path, json);

    }

    public static void LoadManager()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

        string saveString;
        Debug.Log("Loading save file from " + path);
        // Get all save files
        //FileInfo saveFiles = directoryInfo.GetDirectories("Saves.txt");
        //cycle through all save files and identify the most recent one but this is for the case where we have multiple save files, in this case we only have one save file so we can just check if it exists and load it
        if (directoryInfo != null)
        {
            Debug.Log("Save file found in " + path);
            saveString = File.ReadAllText(path);
            DataToSave savedObject = JsonUtility.FromJson<DataToSave>(saveString);
            Debug.Log(savedObject.Currency + " + " + savedObject.health);
            Manager.health = savedObject.health;
            Manager.currency= savedObject.Currency;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }




        #region
        //string path = Application.persistentDataPath + "/Saves.txt";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);

        //    DataToSave data = formatter.Deserialize(stream) as DataToSave;

        //    stream.Close();
        //    return data;
        //}
        //else
        //{
        //    Debug.LogError("Save file not found in " + path);
        //    return null;
        //}
        #endregion
    }

    ////save data to a JSON file with application.persistentDataPath + "/Saves/SaveData.json" as the path
    //string json = JsonUtility.ToJson(this);

    //File.WriteAllText(Application.persistentDataPath + "/Saves/SaveData.json", json);


    

}
