using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private static Menu instance;

    public static Menu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Menu>();
            }
            return instance;
        }
    }

    Button levelButton;

    public SceneFader sceneFader;

    GameManager1 gm;
    Door dr;
    public int nextLevel;
    public int levelnumber;

    [SerializeField]
    public GameObject PauseWindow;

    [SerializeField]
    public GameObject OptionsWindow;

    [SerializeField]
    public GameObject HelpWindow;

    [SerializeField]
    public GameObject CompletedWindow;

    [SerializeField]
    public GameObject MenuUI;

    public enum MenuStates { Playing, Pause, Options, Help, Completed }
    public MenuStates currentState;

    private void Start()
    {
        levelButton = GetComponent<Button>();
        dr = FindObjectOfType<Door>();
        gm = FindObjectOfType<GameManager1>();
    }

    public void Update()
    {
        switch (currentState)
        {
            case MenuStates.Playing:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(false);
                Time.timeScale = 1;
                break;

            case MenuStates.Pause:
                PauseWindow.SetActive(true);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case MenuStates.Options:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(true);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case MenuStates.Help:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(true);
                MenuUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case MenuStates.Completed:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                CompletedWindow.SetActive(true);
                levelButton.interactable = true;
                Time.timeScale = 0;
                break;
        }
    }

    public void SaveButton()
    {
    }

    public void NextLevelButton()
    {
        Door.Instance.SaveScore();
        gm.LoadNextLevel(nextLevel);
    }

    public void LevelSelect()
    {
        gm.LoadNextLevel(levelnumber);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayHelp()
    {
        currentState = MenuStates.Help;
    }

    public void DisplayOptions()
    {
        currentState = MenuStates.Options;
    }

    public void DisplayCompleted()
    {
        currentState = MenuStates.Completed;
    }

    public void Resume()
    {
        currentState = MenuStates.Playing;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        currentState = MenuStates.Pause;
    }

    public void PauseButton()
    {
        currentState = MenuStates.Pause;
    }

    public void SetSFXVolume(float sfxLv)
    {
        AudioManager.instance.SetSFXVolume(sfxLv);
    }

    public void SetMusicVolume(float musicLv)
    {
        AudioManager.instance.SetMusicVolume(musicLv);
    }

    public void SetMasterVolume(float masterLv)
    {
        AudioManager.instance.SetMasterVolume(masterLv);
    }
}
