using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] allRooms;

    List<GameObject> currentRooms;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 7; i++) {
            currentRooms.Add(null);
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        for (int i = 0; i < currentRooms.Count; i++)
        {
            int rand = Random.Range(0, 7);
        }

    }
}
