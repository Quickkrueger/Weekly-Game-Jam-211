using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    public GameObject currentCharacter;
    private BandMember currentMember;
    

    // Start is called before the first frame update
    void Start()
    {
        currentMember = currentCharacter.GetComponent<BandMember>();
        currentMember.SelectMember();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwapCharacter(GameObject character)
    {
        currentMember.DeselectMember();

        currentCharacter = character;
        currentMember = currentCharacter.GetComponent<BandMember>();

        currentMember.SelectMember();
    }

    private void FixedUpdate()
    {
        if (GameController._instance.IsStarted())
        {
            Movement();
            Assign();
        }
    }

   

    private void Interact()
    {

    }

    private void Assign()
    {
        Mouse mouse = Mouse.current;

        if (mouse.rightButton.isPressed && ! currentMember.IsInteracting())
        {
            Debug.Log("StartingInteraction");
            currentMember.BeginInteraction(true);
        }
    }

    private void Movement()
    {
        Keyboard keyboard = Keyboard.current;
        bool wInput = keyboard.wKey.IsPressed();
        bool sInput = keyboard.sKey.IsPressed();

        bool aInput = keyboard.aKey.IsPressed();
        bool dInput = keyboard.dKey.IsPressed();

        float forwardBack = Convert.ToSingle(wInput) - Convert.ToSingle(sInput);
        float leftRight = Convert.ToSingle(dInput) - Convert.ToSingle(aInput);

        currentMember.UpdateMovement(forwardBack, leftRight);
    }
}
