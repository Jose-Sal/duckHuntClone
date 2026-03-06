using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //contains all the important information about the player that we want to save and load, this is the class that will be serialized and deserialized when saving and loading the game
    public static int maxHealth = 100;
    public static int health = 0;
    public static int currency = 1000;

    public UIScript UIScript;

    private void Awake()
    {
        SaveSystem.Init();
    }

    //call the save and load functions from the SaveSystem class when the appropriate buttons are pressed in the UI
    public void SaveGame()
    {
        SaveSystem.SaveManager();
    }
    public void LoadGame()
    {
        SaveSystem.LoadManager();
        UIScript.updateUI();
    }

}
