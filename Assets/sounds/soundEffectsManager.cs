using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class soundEffectsManager : MonoBehaviour
{
    
    [FormerlySerializedAs("camera")] [SerializeField] private CameraScroll cameraScroll;
    [SerializeField] private AudioSource theme;
    [SerializeField] private AudioClip deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGameSounds()
    {
        theme.Play();
    }
    public void playEndgameSounds()
    {
        theme.Pause();
        AudioSource.PlayClipAtPoint(deathSound, cameraScroll.transform.position);
    }
}
