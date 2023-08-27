using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float ScrollSpeed = 0.1f;
    public GameObject player;
    public float DistanceToStartCatchUp = 5f;
    
    private Transform _transform;
    private float _playerDistanceFromCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var distanceThisFrame = ScrollSpeed * Time.fixedDeltaTime;
        _playerDistanceFromCamera = player.transform.position.y - _transform.position.y;

        var startCatchUp = _playerDistanceFromCamera > DistanceToStartCatchUp;
        if (startCatchUp)
        {
            CameraCatchUp(distanceThisFrame);
        }
        else
        {
            _transform.Translate(distanceThisFrame*Vector2.up);
        }
    }

    private void CameraCatchUp(float distanceThisFrame)
    {
        var catchUpIncreaseRate = Mathf.Pow(_playerDistanceFromCamera-DistanceToStartCatchUp, 2);
        _transform.Translate((distanceThisFrame+catchUpIncreaseRate)*Vector2.up);
    }
}
