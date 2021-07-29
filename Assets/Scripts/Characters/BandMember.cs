using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BandMember : MonoBehaviour
{
    Texture icon;

    public int maxHits;//crew member moral
    int currentHits;
    int speed = 5;
    Stats stats;

    public BandMemberScriptableObject memberData;

    MemberType type;

    BaseTask currentTask;
    GameObject currentRoom;

    Rigidbody2D memberRigidbody2D;
    Animator memberAnimator;
    SpriteRenderer memberRenderer;

    float secondsPassed = 0;


    public Image progressBar;
    public Image progressTimer;

    bool useProgress = false;
    bool selected = false;
    private bool horizontal = false;
    private bool moving = false;
    private bool up = false;
    private bool demoralized = false;

    private bool interacting = false;

    private void Start()
    {
        currentHits = maxHits;

        stats = new Stats(0, 0, 0, 0);

        AddBonuses();

        memberAnimator = GetComponent<Animator>();
        memberRigidbody2D = GetComponent<Rigidbody2D>();
        memberRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if(interacting && useProgress && !currentTask.IsCompleted())
        {
            progressBar.fillAmount = currentTask.TaskProgress(Time.deltaTime);
            secondsPassed += Time.deltaTime;
            if ( secondsPassed >= 1 && TestForFailure())
            {
                
                currentHits--;
                progressBar.fillAmount = 0;
                Debug.Log("Failed Task");
                EndInteraction();
                secondsPassed = 0;
            }
            else if(secondsPassed >= 1)
            {
                secondsPassed = 0;
            }
            if(currentHits == 0 && !demoralized)
            {
                demoralized = true;
            }
        }
        else if (interacting && useProgress && currentTask.IsCompleted())
        {
            progressBar.fillAmount = 0;
            EndInteraction();
        }
    }

    private bool TestForFailure()
    {
        int rand = Random.Range(1, Constants.failureChance);

        if(rand > Constants.baseSuccess + stats.GetStatFromIndex((int)currentTask.type)){
            return true;
        }
        return false;
        
    }


    public void SelectMember()
    {
        selected = true;
        memberRigidbody2D.isKinematic = false;
        if(currentRoom != null)
        LevelController._instance.ChangeToRoom(currentRoom);

    }

    public void DeselectMember()
    {
        selected = false;
        memberRigidbody2D.isKinematic = true;
    }

    public void BeginInteraction(bool assigned = false)
    {
        if (currentTask != null && !currentTask.IsCompleted())
        {
            Debug.Log("StartingTask");
            interacting = true;
            progressTimer.enabled = true;
            useProgress = assigned;
        }
    }

    public void EndInteraction()
    {
        if (interacting && useProgress)
        {
            interacting = false;
            useProgress = false;
            progressTimer.enabled = false;
            progressBar.fillAmount = 0;
            currentTask.ResetTask();
        }
    }

    public void UpdateMovement(float forwardBack, float leftRight)
    {

        memberRigidbody2D.velocity = new Vector2(leftRight, forwardBack).normalized * speed;

        if ((Mathf.Abs(forwardBack) > 0) || (Mathf.Abs(leftRight) > 0))
        {
            moving = true;

            if (IsInteracting())
            {
                EndInteraction();
            }
        }
        else
        {
            moving = false;
        }

        if(forwardBack > 0)
        {
            up = true;
        }
        else if(forwardBack < 0)
        {
            up = false;
        }

        if (leftRight < 0)
        {
            memberRenderer.flipX = true;
            horizontal = true;
        }
        else if (leftRight > 0)
        {
            memberRenderer.flipX = false;
            horizontal = true;
        }
        else if (moving)
        {
            horizontal = false;
        }

        memberAnimator.SetBool("moving", moving);

        memberAnimator.SetBool("up", up);

        memberAnimator.SetBool("horizontal", horizontal);
    }

    public void UpdateSprite(string momentString)
    {
        AnimationMoment moment = new AnimationMoment(momentString);
        Sprite[] frames = memberData.frames.GetFramesFromIndex(moment.animationIndex);
        memberRenderer.sprite = frames[moment.frameIndex];
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

[System.Serializable]
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

[System.Serializable]
public struct AnimationMoment
{
    public int animationIndex;
    public int frameIndex;

    public AnimationMoment(string stringMoment)
    {
        string[] parsed = stringMoment.Split(' ');
        animationIndex =int.Parse(parsed[0]);
        frameIndex = int.Parse(parsed[1]);
    }
}
