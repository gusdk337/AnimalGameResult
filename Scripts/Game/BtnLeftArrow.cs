using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BtnLeftArrow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isBtnLeftPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isBtnLeftPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        this.isBtnLeftPressed = false;
    }

}
