using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
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

    public void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached", levelReached);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(int levelnumber)
    {
        fader.FadeTo(levelnumber);
    }

    public void ResetLevelButton()
    {
        PlayerPrefs.SetInt("levelReached", 2);

        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }
}
