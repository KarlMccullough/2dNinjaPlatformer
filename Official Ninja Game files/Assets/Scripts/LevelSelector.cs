using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    private static LevelSelector instance;

    

    public static LevelSelector Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelSelector>();
            }
            return instance;
        }
    }

    public int levelReached;

    public SceneFader fader;

    public Button[] levelButtons;

    public Button resetButton;


    public void Start ()
    {
        levelReached = PlayerPrefs.GetInt("levelReached", levelReached);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {


            if (i + 2 > levelReached)
            {


                levelButtons[i].interactable = false;



            }
        }
        /*if (PlayerPrefs.HasKey("levelReached"))
        {

        }*/
        //levelReached = PlayerPrefs.GetInt("levelReached", 2);
        //levelReached = PlayerPrefs.GetInt("levelReached");




    }

    public void Select(int levelnumber)
    {
       
        fader.FadeTo(levelnumber);

        
        
        

        //SceneFader.Instance.FadeTo(levelnumber);
        
    }

    public void ResetLevelButton()
    {
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
           


            if (i + 8 > levelReached) 
            {
                //levelReached = PlayerPrefs.GetInt("levelReached", 2);
                PlayerPrefs.SetInt("levelReached", 2);

                //levelButtons[i].interactable = false;
                levelButtons[1].interactable = false;
                levelButtons[2].interactable = false;
                levelButtons[3].interactable = false;
                levelButtons[4].interactable = false;



            }
        }
        


        /*levelReached = 2;
        levelReached = PlayerPrefs.GetInt("levelReached", 2);

        levelButtons[1].interactable = false;
        levelButtons[2].interactable = false;
        levelButtons[3].interactable = false;*/


    }




}
