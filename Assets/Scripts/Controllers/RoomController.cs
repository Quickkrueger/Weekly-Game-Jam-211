using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public AvailablePath[] availablePaths;

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
public struct AvailablePath
{
    public Direction direction;
    public GameObject portal;
}

public enum Direction {North, South, East, West};
