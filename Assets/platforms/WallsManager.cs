using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    public List<walls> availableWalls;
    private Transform _transform;
        
    // Start is called before the first frame update
    void Start()
    {
        initializeWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeWalls()
    {
        
        foreach (var wall in availableWalls)
        {
            wall._transform.position = wall.initialCoordination;
        }
    }
}
