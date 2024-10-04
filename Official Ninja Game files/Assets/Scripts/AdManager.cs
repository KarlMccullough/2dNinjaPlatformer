using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;
using GoogleMobileAds.Api;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Admob.Instance().a
        
    }
}
