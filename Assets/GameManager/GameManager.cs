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
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private PlatformGeneration platformGeneration;
    [SerializeField] private WallsManager _wallsManager;
    
    private GameManagerStates _currentStates;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    
    public void StartPlayer()
    {
        _currentStates = GameManagerStates.AwaitingPlayer;
        player.StartMoving();
        sounds.playGameSounds();
        player.PositionForNewGame();
        platformGeneration.initializePlatforms();
        _wallsManager.initializeWalls();
        CameraScroll.instance.PositionForNewGame();
        scoreCounter.StartCounting(player.transform);
    }
    
    public void TryStartGame()
    {
        if (_currentStates == GameManagerStates.AwaitingPlayer)
        {
            _currentStates = GameManagerStates.GameActive;
            cameraScroll.CameraStartScroll();
        }
    }
    
    public void EndGame()
    {
        if (_currentStates == GameManagerStates.GameActive)
        {
            _currentStates = GameManagerStates.GameEnded;
            cameraScroll.CameraStopScroll();
            sounds.playEndgameSounds();
            CameraScroll.instance.CameraStopScroll();
            player.StopMoving();
            scoreCounter.StopCounting();
            mainMenuManager.Open();
        }
    }
}
