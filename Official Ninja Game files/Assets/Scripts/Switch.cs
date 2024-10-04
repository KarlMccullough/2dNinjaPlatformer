using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private static Switch instance;

    public static Switch Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Switch>();
            }
            return instance;
        }
    }

    AudioManager audioManager;

    [SerializeField]
    GameObject switchOn;

    [SerializeField]
    GameObject switchOff;

    public bool isOn = false;

    public bool alreadyplayed = false;

    private void Start()
    {
        audioManager = AudioManager.instance;
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;

       
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!alreadyplayed && col.tag == "Player")
        {
            isOn = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;

            audioManager.PlaySound("Switch Sound");
            alreadyplayed = true;
        }
       
        
        
        
    }
  


}
