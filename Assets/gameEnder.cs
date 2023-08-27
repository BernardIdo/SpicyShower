using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameEnder : MonoBehaviour
{

    [SerializeField] private PlayerController player;
    [SerializeField] private CameraScroll camera;
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
        var playerDistanceFromCamera = player.transform.position.y - camera.transform.position.y;
        _shouldEndGame = playerDistanceFromCamera < _isTooFarEndGameDistance;
        if (_shouldEndGame)
        {
            GameManager.instance.EndGame();
        }
    }
}
