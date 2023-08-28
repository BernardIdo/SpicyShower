using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walls : MonoBehaviour
{
    public Vector2 initialCoordination;
    public Transform _transform;
    private Vector2 centerOfWall;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _transform = transform;
        initialCoordination = transform.position;
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

    public void initializeWalls()
    {
        transform.position = initialCoordination;
    }
}
