using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BandMember : MonoBehaviour
{
    Texture icon;
    public int maxHealth;
    int currentHealth;
    int speed = 5;
    Stats stats;
    public BandMemberScriptableObject memberData;
    MemberType type;
    BaseTask currentTask;
    GameObject currentRoom;

    public Image progressBar;
    bool useProgress = false;
    bool selected = false;

    private bool interacting = false;

    private void Start()
    {
        currentHealth = maxHealth;

        stats = new Stats(0, 0, 0, 0);
    }

    private void Update()
    {
        if(interacting && useProgress && !currentTask.IsCompleted())
        {
            progressBar.fillAmount = currentTask.TaskProgress(Time.deltaTime);
        }
        else if (interacting && useProgress && !currentTask.IsCompleted())
        {
            progressBar.fillAmount = 0;
            EndInteraction();
        }
    }

    public void SelectMember()
    {
        selected = true;
        LevelController._instance.ChangeToRoom(currentRoom);

    }

    public void DeselectMember()
    {
        selected = false;

    }

    public void BeginInteraction(bool assigned = false)
    {
        if (currentTask != null && !currentTask.IsCompleted())
        {
            Debug.Log("StartingTask");
            interacting = true;

            useProgress = assigned;
        }
    }

    public void EndInteraction()
    {
        if (interacting && useProgress)
        {
            interacting = false;
            useProgress = false;
            progressBar.fillAmount = 0;
            currentTask.ResetTask();
        }
    }

    public bool IsInteracting()
    {
        return interacting;
    }

    private void AddBonuses()
    {
        type = memberData.memberType;
        if (memberData.statBoosts.talent)
        {
            stats.talent++;
        }
        if (memberData.statBoosts.technical)
        {
            stats.technical++;
        }
        if (memberData.statBoosts.finesse)
        {
            stats.finesse++;
        }
        if (memberData.statBoosts.hardiness)
        {
            stats.hardiness++;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            currentRoom = collision.gameObject;
            if (selected)
            {
                LevelController._instance.ChangeToRoom(currentRoom);
            }
            Debug.Log("Hello");
        }

        if (collision.tag == "Task")
        {
            currentTask = collision.gameObject.GetComponent<BaseTask>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Task" && currentTask == collision.gameObject.GetComponent<BaseTask>())
        {
            currentTask = null;
        }
    }


}

public struct Stats
{
    public Stats(int tal, int tech, int fin, int hard)
    {
        talent = tal;
        technical = tech;
        finesse = fin;
        hardiness = hard;
    }

    public int GetStatFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return talent;
            case 1:
                return technical;
            case 2:
                return finesse;
            case 3:
                return hardiness;
            default:
                return -1;
        }
    }

    public int talent;
    public int technical;
    public int finesse;
    public int hardiness;
}
