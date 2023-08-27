using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private Transform _transform;
    public float ScrollSpeed = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var distanceThisFrame = ScrollSpeed * Time.fixedDeltaTime;
        _transform.Translate(distanceThisFrame*Vector2.up); 
    }
}
