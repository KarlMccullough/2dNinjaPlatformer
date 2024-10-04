using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;
    
    
    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public Player player;
    

    public bool isActive;

    public bool stopPlayer;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
       

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive == true)
        {
            EnableTextBox();
        }
        else if (isActive == false)
        {
            DisableTextBox();
        }


    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        //theText.text = textLines[currentLine];

        if (CrossPlatformInputManager.GetButtonDown("mouse 0") || Input.GetKeyDown("mouse 0"))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }


    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        StartCoroutine(TextScroll(textLines[currentLine]));
        if (stopPlayer == true)
        {
            
            player.canMove = false;
            
        }
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;

        player.canMove = true;

    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }

}

