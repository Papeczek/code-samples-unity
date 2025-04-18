using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.EventSystems;

public class RepositionableJoystick : MMTouchRepositionableJoystick
{
    public Vector2 LastRawValue;
    public float LastMagnitude;

    public override void OnPointerUp(PointerEventData eventData)
    {
        LastRawValue = RawValue;
        LastMagnitude = Magnitude;
        base.OnPointerUp(eventData);
    }
}
