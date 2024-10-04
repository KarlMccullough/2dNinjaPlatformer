using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState states;

    private void Awake()
    {
        Instance = this;
        Load();
    }

    public void Save()
    {
       // PlayerPrefs.SetString("save", )
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
         //   states = 
        }
        else
        {
            states = new SaveState();
            Save();
            Debug.Log("No svae file found. Creating a new one");
        }
    }

}
