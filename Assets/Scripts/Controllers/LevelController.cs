using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{
    public static LevelController _instance;
    public CharacterController characterController;
    public GameObject[] bandMembers;
    //public GameObject[] rooms;
    private int memberIndex = 0;
    private bool swapping = false;
    private RoomController currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LevelInputs();
    }

    public void ChangeToRoom(GameObject room)
    {
        RoomController newRoom = room.GetComponent<RoomController>();

        if (currentRoom != null)
        {
            currentRoom.roomCamera.enabled = false;
        }

        currentRoom = newRoom;

        currentRoom.roomCamera.enabled = true;
        

    }

    private void LevelInputs()
    {
        Keyboard keyboard = Keyboard.current;

        bool eInput = keyboard.eKey.isPressed;
        bool qInput = keyboard.qKey.isPressed;

        if(!eInput && !qInput)
        {
            swapping = false;
        }

        if (eInput && !swapping)
        {
            swapping = true;
            SwapCharacter(true);
        }
        else if (qInput && !swapping)
        {
            swapping = true;
            SwapCharacter(false);
        }
    }

    private void SwapCharacter(bool next)
    {
        if (next)
        {
            memberIndex++;
            if(memberIndex >= bandMembers.Length)
            {
                memberIndex = 0;
            }
        }
        else
        {
            memberIndex--;
            if (memberIndex < 0)
            {
                memberIndex = bandMembers.Length - 1;
            }
        }

        characterController.SwapCharacter(bandMembers[memberIndex]);

    }


}
