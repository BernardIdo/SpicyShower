using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class gameEnder : MonoBehaviour
{

    [SerializeField] private PlayerController player;
    [FormerlySerializedAs("camera")] [SerializeField] private CameraScroll cameraScroll;
    private bool _shouldEndGame;
    private float _isTooFarEndGameDistance = -12f;
    
    // Start is called before the first frame update
    void Start()
    {
        _shouldEndGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        var playerDistanceFromCamera = player.transform.position.y - cameraScroll.transform.position.y;
        _shouldEndGame = playerDistanceFromCamera < _isTooFarEndGameDistance;
        if (_shouldEndGame)
        {
            GameManager.instance.EndGame();
        }
    }
}
