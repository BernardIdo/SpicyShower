using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float ScrollSpeed = 0.1f;
    
    private Transform _transform;
    public GameObject player;
    
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
        
        Debug.Log("Player position is "+player.transform.position);
    }
    
    
}
