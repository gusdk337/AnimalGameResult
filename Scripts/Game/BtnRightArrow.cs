using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BtnRightArrow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isBtnRightPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isBtnRightPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        this.isBtnRightPressed = false;
    }

}
