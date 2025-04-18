using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick_SwipeZoneSwitch : MonoBehaviour
{
    [SerializeField] private Image joystickRepositionImage;
    [SerializeField] private GameObject swipeZoneGO;

    private void Start()
    {
        joystickRepositionImage.enabled = true;
        swipeZoneGO.SetActive(false);
    }
    public void Joystick_SwipeZoneToggle()
    {
        bool joystickEnabled = !joystickRepositionImage.enabled;
        joystickRepositionImage.enabled = joystickEnabled;
        swipeZoneGO.SetActive(!joystickEnabled);
    }
}
