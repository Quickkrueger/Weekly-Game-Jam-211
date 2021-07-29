using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController _instance;

    public Text timeText;
    public float maxTime;
    public GameObject tablet;
    float timeRemaining;
    float initialPosition;
    public float tabletSpeed;
    public float tabletOffset;
    public BaseTask[] tasks;
    public Text[] taskText;
    public BaseTask[] doorTasks;
    public GameObject victoryScreen;
    public GameObject failureScreen;
    public GameObject menu;

    bool started = false;
    bool tabletMoving = false;
    bool tabletOut = false;
    bool victory = false;
    bool failure = false;


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        timeRemaining = maxTime;
        initialPosition = tablet.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (started && !victory && !failure)
        {
            TickDown();
            CheckForInput();
            if (tabletMoving)
            {
                MoveTablet();
            }
                UpdateTasks();
        }
        if(victory || failure)
        {
            CheckForRestart();
        }
    }

    public bool IsStarted()
    {
        return started;
    }

    public void StartGame()
    {
        started = true;
        menu.SetActive(false);
    }

    void SetEndscreen()
    {
        if (victory)
        {
            victoryScreen.SetActive(true);
        }
        else if (failure)
        {
            failureScreen.SetActive(true);
        }
    }

    void UpdateTasks()
    {
        bool allComplete = true;
        for(int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i].IsCompleted())
            {
                taskText[i].color = Color.green;
            }
            if (!tasks[i].IsCompleted())
            {
                allComplete = false;
            }
        }
        victory = allComplete;
        if (victory)
        {
            SetEndscreen();
        }

    }

    void MoveTablet()
    {
        float speed = tabletSpeed * Screen.height * tabletOffset;

        if (tabletOut)
        {
            if(tablet.transform.position.y > initialPosition - (Screen.height * tabletOffset))
            {
                

                tablet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * -1f);
            }
            else
            {
                tablet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0f);
                tabletMoving = false;
            }
        }
        else if (!tabletOut)
        {
            if (tablet.transform.position.y < initialPosition)
            {
                tablet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else
            {
                tablet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0f);
                tabletMoving = false;
            }
        }
    }

    public void CompleteDoors()
    {
        for(int i = 0; i < doorTasks.Length; i++)
        {
            doorTasks[i].CompleteTask();
        }
    }

    void CheckForInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.tabKey.isPressed && !tabletMoving && !tabletOut)
        {
            tabletMoving = true;
            tabletOut = true;
        }
        else if (keyboard.tabKey.isPressed && !tabletMoving && tabletOut)
        {
            tabletMoving = true;
            tabletOut = false;
        }
    }

    void CheckForRestart()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.escapeKey.isPressed)
        {
            SceneManager.LoadScene(0);
        }
    }

    void UpdateClock()
    {
        int mins = (int)timeRemaining / 60;
        int secs = (int)timeRemaining % 60;
        string minString = mins.ToString();
        string secString;
        if(secs < 10)
        {
            secString = 0 + secs.ToString();
        }
        else
        {
            secString = secs.ToString();
        }
        timeText.text = minString + ":" + secString;
    }

    void TickDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateClock();
        }
        else
        {
            failure = true;
            SetEndscreen();
        }
    }
}

