using System;
using moving;
using moving.impl;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UIInputSource inputSource;
    [SerializeField] private InputCode inputCode;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(1);
        inputSource.UpdateState(inputCode, false);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        inputSource.UpdateState(inputCode, true);
    }
}