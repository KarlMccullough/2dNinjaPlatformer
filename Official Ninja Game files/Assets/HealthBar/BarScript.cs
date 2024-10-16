﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

   
    
    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private float lerpspeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text ValueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = ValueText.text.Split(':');
            ValueText.text = tmp[0] + ":" + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
           
        }
        
    }

    // Use this for initialization
    void Start () {
      

    }
	
	// Update is called once per frame
	void Update () {
        

        HandleBar();
	}

    private void HandleBar()
    {
        
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpspeed);

        }
       

    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    

    
}
