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
            CameraScroll.instance.CameraStartScroll();
        }
        
    }
    
    public void EndGame()
    {
        if (currentStates == GameManagerStates.GameActive)
        {
            currentStates = GameManagerStates.GameEnded;
            sounds.playEndgameSounds();
            CameraScroll.instance.CameraStopScroll();
            player.StopMoving();
            mainMenuManager.Open();
        }
    }
}
