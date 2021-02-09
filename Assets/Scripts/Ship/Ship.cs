using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //Movement
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public float moveSpeed;

    //Aiming
    private Camera cam;

    public SpriteRenderer bodyRend;
    public Sprite front;
    public Sprite back;

    public Transform body;
    public Transform shadow;


    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Move Input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
    }

        void FixedUpdate()
    {
        //Aim
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(body.transform.localPosition);

        CheckAimDirection(mousePos, screenPoint);

        //Rotate Arm -> Pass angle
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        //rotate angle
        //body.rotation  = Quaternion.Euler(0, 0, angle);
        //shadow.rotation  = Quaternion.Euler(0, 0, angle);

        //move
        rb.velocity = moveInput * moveSpeed;
    }

        void CheckAimDirection(Vector3 mousePos, Vector3 screenPoint)
    {
        //is the mouse right of the player?
        if(mousePos.y > screenPoint.y)
        {
            bodyRend.sprite = back;
        }
        else
        {
            bodyRend.sprite = front;
        }


                //is the mouse above the player?
        if(mousePos.x > screenPoint.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
