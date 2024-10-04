using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class stat
{
    [SerializeField]
    private BarScript bar;

    //[SerializeField]
    //private BarScript XPbar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;

    //[SerializeField]
    //private float xPCurrentVal;



    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {

            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
            
            
        }
    }


   /* public float XPCurrentVal
    {
        get
        {
            return xPCurrentVal;
        }

        set
        {

            this.xPCurrentVal = Mathf.Clamp(value, 0, MaxVal);
            XPbar.Value = xPCurrentVal;


        }
    }*/

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            
            maxVal = value;
            bar.MaxValue = value;
            //XPbar.MaxValue = value;
        }
    }
    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
        //this.XPCurrentVal = xPCurrentVal;

    }

    
}
