using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour {

    //NINJAS RIGID BODY AND ANIMATOR
    private Rigidbody2D ninjaRigidBody;
    private Animator ninjaAnimator;

    //MOVEMENT AND CHARACTER FLIPPING
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;

    //JUMPING
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatsGround;
    private bool ground;
    private bool jump;
    [SerializeField]
    private float jumpForce;
    private bool doubleJump;

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
        handleLayers();
        ground = isGrounded();
        handleMovement(horizontal);
        flip(horizontal);
        resetValues();
    }

    private void handleMovement(float horizontal)
    {
        if (ninjaRigidBody.velocity.y < 0)
        {
            ninjaAnimator.SetBool("landed", true);
        }
        ninjaRigidBody.velocity = new Vector2(horizontal * movementSpeed, ninjaRigidBody.velocity.y);
        ninjaAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (ground && jump)
        {
            Debug.Log("Ground and jump");
            ground = false;
            ninjaAnimator.SetTrigger("jump");
            ninjaRigidBody.AddForce(new Vector2(0, jumpForce));
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMP");
            jump = true;
        }
    }

    private void resetValues()
    {
        jump = false;
    }

    private bool isGrounded()
    {
        if(ninjaRigidBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] coliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatsGround);

                for(int i=0; i<coliders.Length; i++)
                {
                    if(coliders[i].gameObject != gameObject)
                    {
                        ninjaAnimator.ResetTrigger("jump");
                        ninjaAnimator.SetBool("landed", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void handleLayers()
    {
        if (!ground)
        {
            ninjaAnimator.SetLayerWeight(1, 1);
        }
        else{
            ninjaAnimator.SetLayerWeight(0, 1);
        }
    }
}
