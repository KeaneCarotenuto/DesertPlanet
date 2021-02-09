using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterVisuals : MonoBehaviour
{
    public Sprite front;
    public Sprite back;
    public Sprite portrait;
    //Sprites
    public SpriteRenderer bodyRend;

    //Animation
    private Animator anim;
    public bool isMoving;

    //UI
    public Image displayPortrait;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isMoving == true)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void UpdateVisuals(
        Sprite newFront, 
        Sprite newBack, 
        Sprite newPortrait
        )
    {
        front = newFront;
        back = newBack;
        portrait = newPortrait;
        displayPortrait.sprite = portrait;

        bodyRend.sprite = front;
    }



    public void FlipAndTurn(bool xDirection, bool yDirection)
    {
        //is the mouse above the player?
        if(xDirection)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //is the mouse right of the player?
        if(yDirection)
        {
            bodyRend.sprite = back;
        }
        else
        {
            bodyRend.sprite = front;
        }
    }
}
