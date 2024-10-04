using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thronsScript : MonoBehaviour {

    private Player player;
    //private Character chara;

    //public GameObject player;
    public void Start()
    {
        player = FindObjectOfType<Player>();
        //chara = FindObjectOfType<Character>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            player.TakeDamage();
            
        }
    } 
}
