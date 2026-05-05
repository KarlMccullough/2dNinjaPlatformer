using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // TODO: Implement new Google Mobile Ads SDK integration for Unity 6
    // See: https://developers.google.com/admob/unity/quick-start
}
