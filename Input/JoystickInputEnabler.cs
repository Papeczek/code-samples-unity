using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
public class JoystickInputEnabler : MonoBehaviour
{
    
    private CanvasGroup joystickCanvasGroup;
    private void Start()
    {
        joystickCanvasGroup = GUIManagerSplashScreen.Instance.Joystick;
    }
    public void EnableJoystick()
    {
        joystickCanvasGroup.alpha = 1f;
        InputManager.Instance.InputDetectionActive = true;
    }
    public void DisableJoystick()
    {
        joystickCanvasGroup.alpha = 0f;
        InputManager.Instance.InputDetectionActive = false;
    }
}
