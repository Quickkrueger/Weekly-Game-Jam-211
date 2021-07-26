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
    private SpriteRenderer currentRenderer;

    private bool interacting = false;
    private bool horizontal = false;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        currentRigidbody2D = currentCharacter.GetComponent<Rigidbody2D>();
        currentAnimator = currentCharacter.GetComponent<Animator>();
        currentMember = currentCharacter.GetComponent<BandMember>();
        currentCollider = currentCharacter.GetComponent<Collider2D>();
        currentRenderer = currentCharacter.GetComponent<SpriteRenderer>();

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

    private void UpdateAnimation(float forwardBack, float leftRight)
    {
        

        if((Mathf.Abs(forwardBack) > 0) || (Mathf.Abs(leftRight) > 0)){
            moving = true;
        }
        else
        {
            moving = false;
        }

        if(leftRight < 0)
        {
            currentRenderer.flipX = true;
            horizontal = true;
        }
        else if(leftRight > 0)
        {
            currentRenderer.flipX = false;
            horizontal = true;
        }
        else if(moving)
        {
            horizontal = false;
        }

        currentAnimator.SetBool("moving", moving);

        currentAnimator.SetBool("horizontal", horizontal);
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

        UpdateAnimation(forwardBack, leftRight);
    }
}
