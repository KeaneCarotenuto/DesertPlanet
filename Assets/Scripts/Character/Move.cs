using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;
    public Rigidbody2D rb;
    private CharacterInformation characterInformation;
    // Start is called before the first frame update
    void start()
    {
        characterInformation = GetComponent<CharacterInformation>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MovementVelocity(Vector3 velocityInput)
    {
        this.velocityVector = velocityInput;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocityVector * moveSpeed;

        //character.PlayMoveAnim(velocityVector);
    }
}
