using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{

    public List<platform> availablePlatforms;
    private List<platform> activePlatforms;
        
    // Start is called before the first frame update
    void Start()
    {
        foreach (var platform in availablePlatforms)
        {
            platform.onPlatformOutOfScreen -= HandlePlatformExitScreen;
            platform.onPlatformOutOfScreen += HandlePlatformExitScreen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegeneratePlatform(platform platform, float difficulty)
    {
        
    }

    private void HandlePlatformExitScreen(platform platform)
    {
        platform._transform.position += Vector3.up * 36;
    }
}
