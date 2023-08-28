using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class platform : MonoBehaviour
{

    [HideInInspector] public float platformLength;
    [HideInInspector] public float distanceFromPreviousPlatform;
    [HideInInspector] public Transform _transform;
    public Action<platform> onPlatformOutOfScreen;
    
    private Vector2 centerOfPlatform;
    private float distanceFromCamera;
    
    // Start is called before the first frame update
    void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        centerOfPlatform = transform.position;
        distanceFromCamera = CameraScroll.instance.transform.position.y - centerOfPlatform.y;
        
        var isOutOfScreen = distanceFromCamera > 12f;
        if (isOutOfScreen)
        {
            onPlatformOutOfScreen.Invoke(this);
        }
    }
}
