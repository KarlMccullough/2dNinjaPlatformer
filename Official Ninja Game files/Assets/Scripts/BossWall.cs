﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FindObjectOfType<Enemy>())
        {
            return;
        }

        Destroy(gameObject);
	}
}
