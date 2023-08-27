using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameManagerStates
    {
        MainMenu = 0,
        AwaitingPlayer = 1,
        GameActive = 2,
        Paused = 3,
        GameEnded = 4,
    }
    
    public static GameManager instance;
    [SerializeField] private CameraScroll camera;
    [SerializeField] private PlayerController player;
    [SerializeField] private soundEffectsManager sounds;
    private GameManagerStates currentStates;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartPlayer()
    {
        currentStates = GameManagerStates.AwaitingPlayer;
        player.StartMoving();
        sounds.playGameSounds();
    }
    
    public void TryStartGame()
    {
        if (currentStates == GameManagerStates.AwaitingPlayer)
        {
            currentStates = GameManagerStates.GameActive;
            camera.CameraStartScroll();
        }
        
    }
    
    public void EndGame()
    {
        if (currentStates == GameManagerStates.GameActive)
        {
            currentStates = GameManagerStates.GameEnded;
            camera.CameraStopScroll();
            sounds.playEndgameSounds();
            player.StopMoving();
        }
    }
}
