using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    public GameObject currentCharacter;
    private Rigidbody2D currentRigidbody2D;
    private Animator currentAnimator;
    private BandMember currentMember;
    private Collider2D currentCollider;

    private bool interacting = false;

    // Start is called before the first frame update
    void Start()
    {
        currentRigidbody2D = currentCharacter.GetComponent<Rigidbody2D>();
        currentAnimator = currentCharacter.GetComponent<Animator>();
        currentMember = currentCharacter.GetComponent<BandMember>();
        currentCollider = currentCharacter.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwapCharacter(GameObject character)
    {
        currentCollider.enabled = false;

        currentCharacter = character;
        currentRigidbody2D = currentCharacter.GetComponent<Rigidbody2D>();
        currentAnimator = currentCharacter.GetComponent<Animator>();
        currentMember = currentCharacter.GetComponent<BandMember>();
        currentCollider = currentCharacter.GetComponent<Collider2D>();

        currentCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        Movement();
        Assign();
    }

    private void Interact()
    {

    }

    private void Assign()
    {
        Mouse mouse = Mouse.current;

        if (mouse.rightButton.isPressed && ! interacting)
        {
            interacting = true;
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

        float speed = currentMember.GetSpeed();

        currentRigidbody2D.velocity = new Vector2(leftRight, forwardBack).normalized * speed;
    }
}
