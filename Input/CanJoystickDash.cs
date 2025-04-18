using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
public class CanJoystickDash : MonoBehaviour
{
    [SerializeField] private float minMagnitude = 0.6f;
    [SerializeField] private RepositionableJoystick joystick;
    void Start()
    {
        joystick = GetComponent<RepositionableJoystick>();    
    }

    public void OnPointerNotAtStartPositionUp()
    {
       
        if(joystick.LastMagnitude >= minMagnitude)
        {
            InputManager.Instance.DashButtonDown();
        }
    }
}
