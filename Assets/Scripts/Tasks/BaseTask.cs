using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTask : MonoBehaviour
{
    public float baseTime;
    public TaskType type;
    float progressedTime = 0;
    bool completed = false;

    SpriteRenderer sprite;

    GameObject minigame;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
                sprite.color = Color.green;

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

public enum TaskType { Technical, Finesse, Hardiness };
