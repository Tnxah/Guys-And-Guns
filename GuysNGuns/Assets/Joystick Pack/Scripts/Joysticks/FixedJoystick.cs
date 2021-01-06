using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if(transform.name == "Aim Joystick")
        {
            multitude = 0;
            handle.anchoredPosition = Vector2.zero;
        }
        else
        base.OnPointerUp(eventData);
    }
}