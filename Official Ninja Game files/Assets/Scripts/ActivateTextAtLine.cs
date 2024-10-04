using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public Door door;

    public bool destroyWhenActivated;
   

    // Use this for initialization
    void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();
		door = FindObjectOfType<Door>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           
            
            theTextBox.isActive = true;
            
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
           
            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }

        }
        
    }
}
