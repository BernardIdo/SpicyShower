using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    [FormerlySerializedAs("camera")] [SerializeField] private CameraScroll cameraScroll;
    [SerializeField] private PlayerController player;
    [SerializeField] private soundEffectsManager sounds;
    [SerializeField] private MainMenuManager mainMenuManager;
    private GameManagerStates currentStates;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
            cameraScroll.CameraStartScroll();
        }
        
    }
    
    public void EndGame()
    {
        if (currentStates == GameManagerStates.GameActive)
        {
            currentStates = GameManagerStates.GameEnded;
            cameraScroll.CameraStopScroll();
            sounds.playEndgameSounds();
            player.StopMoving();
            mainMenuManager.Open();
        }
    }
}
