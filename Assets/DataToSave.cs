using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//save it into a file
[System.Serializable]
public class DataToSave 
{
    public int health;
    public int Currency;

    public DataToSave()
    {
        health = Manager.health;
        Currency = Manager.currency;
    }
}
