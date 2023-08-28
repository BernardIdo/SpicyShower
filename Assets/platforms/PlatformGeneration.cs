using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{

    public List<platform> availablePlatforms;
    
    private List<platform> activePlatforms;
    private Vector3 topPlatformPosition;
    public float difficulty;
        
    // Start is called before the first frame update
    void Start()
    {
        topPlatformPosition = Vector3.zero;
        difficulty = 0;
        
        foreach (var platform in availablePlatforms)
        {
            platform.onPlatformOutOfScreen -= HandlePlatformExitScreen;
            platform.onPlatformOutOfScreen += HandlePlatformExitScreen;

            platform._transform.position = topPlatformPosition;
            topPlatformPosition = getNextPlatformPosition(platform);
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
        platform._transform.position = getNextPlatformPosition(platform);
        topPlatformPosition = platform._transform.position;
    }

    private Vector2 getNextPlatformPosition(platform platform)
    {
        var stepUp = Random.Range(2 + difficulty, 6 + difficulty);
        var stepSideways = Random.Range(-6+platform.platformLength/2, 6-platform.platformLength/2);

        var platformNewHeight = Vector2.up*(topPlatformPosition.y + stepUp);
        var platformNewHorizontal = Vector2.right * (stepSideways);

        return (platformNewHeight + platformNewHorizontal);
    }
}
