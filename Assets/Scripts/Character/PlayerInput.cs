using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Movement
    private Vector2 moveInput;

    private Move move;

    //Aiming
    private Camera cam;
    public CharacterVisuals characterVisuals;
    private bool xDirection;
    private bool yDirection;


    private void Awake()
    {
        cam = Camera.main;

        move = GetComponent<Move>();
        characterVisuals = GetComponent<CharacterVisuals>();
    }
    

    private void Update()
    {
        //Move Input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        move.MovementVelocity(moveInput);

        if(moveInput != Vector2.zero)
        {
            characterVisuals.isMoving = true;
        }
        else
        {
            characterVisuals.isMoving = false;
        }
    }

        void FixedUpdate()
    {
        //Aim
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        CheckAimDirection(mousePos, screenPoint);

    }

        void CheckAimDirection(Vector3 mousePos, Vector3 screenPoint)
    {
        //is the mouse above the player?
        if(mousePos.x > screenPoint.x)
        {
            xDirection = true;
        }
        else
        {
            xDirection = false;
        }

        //is the mouse right of the player?
        if(mousePos.y > screenPoint.y)
        {
            yDirection = true;
        }
        else
        {
            yDirection = false;
        }

        //Update Visuals
        characterVisuals.FlipAndTurn(xDirection, yDirection);
    }
}
