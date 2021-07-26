using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public AvailablePaths availablePaths;

    public CinemachineVirtualCamera roomCamera;
    public BaseTask[] tasks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct AvailablePaths
{
    public bool north;
    public bool south;
    public bool east;
    public bool west;
}
