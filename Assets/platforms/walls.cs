using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walls : MonoBehaviour
{
    private Transform _transform;
    private Vector2 centerOfWall;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        centerOfWall = transform.position;
        var distanceFromCamera = CameraScroll.instance.transform.position.y - centerOfWall.y;
        
        var isOutOfScreen = distanceFromCamera > 0f;
        if (isOutOfScreen)
        {
            transform.position += Vector3.up * 20f;
        }
    }
}
