using UnityEngine;
using UnityEngine.UI;

public class RotateDevicePrompt : MonoBehaviour
{
    [SerializeField]
    private GameObject rotateMessage;

    void Update()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Screen.width < Screen.height)
        {
            rotateMessage.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            rotateMessage.SetActive(false);
            Time.timeScale = 1;
        }
#endif
    }
}
