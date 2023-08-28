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
        foreach (var platform in availablePlatforms)
        {
            platform.onPlatformOutOfScreen -= HandlePlatformExitScreen;
            platform.onPlatformOutOfScreen += HandlePlatformExitScreen;
        }

        topPlatformPosition = 35 * Vector3.up;
        difficulty = 0;
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
        var stepUp = Random.Range(2 + difficulty, 6 + difficulty);
        var stepSideways = Random.Range(-6+platform.platformLength/2, 6-platform.platformLength/2);

        var platformNewHeight = Vector2.up*(topPlatformPosition.y + stepUp);
        var platformNewHorizontal = Vector2.right * (stepSideways);

        platform._transform.position = platformNewHeight + platformNewHorizontal;
        topPlatformPosition = platform._transform.position;
        
   

    }
}
