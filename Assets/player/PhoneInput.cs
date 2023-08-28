using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneInput : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler
{
    public static PhoneInput instance;
    
    public Action<bool> onDrag;
    public Action onRelease;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        
        var isLeft = (eventData.position.x - 0.5f * Screen.width) < 0;
        onDrag?.Invoke(isLeft);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onRelease?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
