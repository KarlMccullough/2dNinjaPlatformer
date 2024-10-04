using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

    private static GameManager1 instance;

    Door dr;

    AudioManager audioManager;

    [SerializeField]
    private GameObject coinPrefab;

    public string nextLevel = "Level02";
    public int levelToUnlock = 3;
    public SceneFader sceneFader;
    

    [SerializeField]
    private Text coinTxt;

    [SerializeField]
    GameObject[] switches;

    [SerializeField]
    GameObject exitDoor;

    public int noOfSwitches;   //= 0;

    [SerializeField]
    Text switchCount;

    public AudioSource hurtSound;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            collectedCoins = PlayerPrefs.GetInt("Score");
        }
     

        dr = FindObjectOfType<Door>();
        audioManager = AudioManager.instance;

        GetNoOfSwitches();
    }


    public static GameManager1 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager1>();
            }
            return instance;
        }
    }

    public GameObject CoinPrefab
    {
        get
        {
            return coinPrefab;
        }
    }

    public int CollectedCoins
    {
        get
        {
            audioManager.PlaySound("Coin Sound");
            return collectedCoins;
           
        }

        set
        {
            coinTxt.text = value.ToString();
            collectedCoins = value;
        }
    }

    public int collectedCoins;

    public void LoadNextLevel(int x)
    {
       
        SceneManager.LoadScene(x);
    }

  
   

   
    public int GetNoOfSwitches()
    {
        int x = 0;

        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetComponent<Switch>().isOn == false)
            {
                x++;
            }
            else if (switches[i].GetComponent<Switch>().isOn == true)
            {
                noOfSwitches--;
            }
        }
        noOfSwitches = x;

        return noOfSwitches;
    }

    public void GetExitDoorState()
    {
        if (noOfSwitches <= 0)
        {
            exitDoor.GetComponent<Door>().OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {


        switchCount.text = GetNoOfSwitches().ToString();

        GetExitDoorState();
    }

    public void WinLvl()
    {

        //PlayerPrefs.GetInt("PlayerScore", CollectedCoins);
        

        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        Menu.Instance.currentState = Menu.MenuStates.Completed;
        
    }

}
