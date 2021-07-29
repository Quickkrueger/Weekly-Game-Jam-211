using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTask : BaseTask
{
    // Start is called before the first frame update
    public BaseTask[] tasks;

    private void Update()
    {
        completed = CheckTasks();
    }

    bool CheckTasks()
    {
        if (!completed)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                if (!tasks[i].IsCompleted())
                {
                    return false;
                }
            }
        }
        return true;
    }
}
