using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTask : MonoBehaviour
{
    public float baseTime;
    public TaskType type;
    float progressedTime = 0;
    bool completed = false;
    public string taskDescription;

    GameObject minigame;
    public GameObject sparks;
    public GameObject door;

    private void Start()
    {
    }

    public float TaskProgress(float deltaTime)
    {
            float progress;
            progressedTime += deltaTime;

            progress = progressedTime / baseTime;

            if (progress >= 1)
            {
                progress = 1;
                completed = true;
            sparks.SetActive(false);
            if(door != null)
            {
                door.GetComponent<Collider2D>().enabled = false;
                door.GetComponent<SpriteRenderer>().color = Color.white;
            }

            }

            return progress;
        
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public void ResetTask()
    {
        progressedTime = 0;
    }



}

public enum TaskType { Talent, Technical, Finesse, Hardiness };
