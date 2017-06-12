using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour {

    private Rigidbody2D ninjaRigidBody;
    private Animator ninjaAnimator;

    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    private bool attack;


	// Use this for initialization
	void Start () {
        facingRight = true;
        ninjaRigidBody = GetComponent<Rigidbody2D>();
        ninjaAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame, and it is not the correct way to move a character
    void Update()
    {
        handleInput();
    }

    //FixedUpdate is called once per TimeStamp (computer time) and is the correct way to move
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        handleMovement(horizontal);
        flip(horizontal);
    }

    private void handleMovement(float horizontal)
    {
        ninjaRigidBody.velocity = new Vector2(horizontal * movementSpeed, ninjaRigidBody.velocity.y);
        ninjaAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void flip(float horizontal)
    {
        Vector3 scale = transform.localScale;
        if (horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            scale.x *= -1;
        }
        else if (horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            scale.x *= -1;
        }
        transform.localScale = scale;
    }

    private void handleInput()
    {
       
    }



}
