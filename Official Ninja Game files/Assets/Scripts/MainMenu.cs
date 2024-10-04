using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

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
        /*if (PlayerPrefs.HasKey("Score"))
        {
            scoreText.text = PlayerPrefs.GetInt("Score").ToString();
            //gm.collectedCoins = PlayerPrefs.GetInt("Score");

        }*/
    }
    public void ResetCoins()
    {
        PlayerPrefs.SetInt("Score", 0);
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }



    public void PlayGame(int levelnumber)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //scene.FadeTo(levelnumber);
    }
    public void Exit()
    {
        // SceneManager.LoadScene(0);
        //PlayerPrefs.SetInt("levelReached", 2);

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
