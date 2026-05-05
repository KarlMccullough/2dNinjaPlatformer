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
        string json = JsonUtility.ToJson(states);
        PlayerPrefs.SetString("save", json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            states = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            states = new SaveState();
            Save();
            Debug.Log("No save file found. Creating a new one");
        }
    }
}
