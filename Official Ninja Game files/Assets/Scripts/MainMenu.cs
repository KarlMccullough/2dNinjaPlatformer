using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text scoreText;

    Door dr;
    GameManager1 gm;

    SceneFader scene;

    private int score;

    private void Start()
    {
        dr = FindObjectOfType<Door>();
        gm = FindObjectOfType<GameManager1>();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetInt("Score", 0);
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void PlayGame(int levelnumber)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
