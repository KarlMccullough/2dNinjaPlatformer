using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string Name;

    public void OnPointerDown(PointerEventData eventData)
    {
        CrossPlatformInputManager.SetButtonDown(Name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CrossPlatformInputManager.SetButtonUp(Name);
    }

    public void SetDownState()
    {
        CrossPlatformInputManager.SetButtonDown(Name);
    }

    public void SetUpState()
    {
        CrossPlatformInputManager.SetButtonUp(Name);
    }

    public void SetAxisPositiveState()
    {
        CrossPlatformInputManager.SetAxisPositive(Name);
    }

    public void SetAxisNeutralState()
    {
        CrossPlatformInputManager.SetAxisZero(Name);
    }

    public void SetAxisNegativeState()
    {
        CrossPlatformInputManager.SetAxisNegative(Name);
    }
}
