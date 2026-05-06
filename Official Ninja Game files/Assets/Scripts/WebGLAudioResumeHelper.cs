using UnityEngine;
using System.Runtime.InteropServices;

public static class WebGLAudioResumeHelper
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WebGLResumeAudio();

    [DllImport("__Internal")]
    private static extern void WebGLInitAudioResume();
#endif

    public static void Init()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLInitAudioResume();
#endif
    }

    public static void ResumeAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLResumeAudio();
#endif
    }
}
