using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour {

    //NINJAS RIGID BODY AND ANIMATOR
    private Animator ninjaAnimator;
    public Rigidbody2D NinjaBody { get; set; }

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
    [SerializeField]
    private float jumpForce;

    //Singleton
    private static Ninja instance;
    public static Ninja Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Ninja>();
            }
            return instance;
        }
    }
    //Properties
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    void Start () {
        facingRight = true;
        NinjaBody = GetComponent<Rigidbody2D>();
        ninjaAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame, and it is not the correct way to move a character
    void Update()
    {
        handleInput();
        OnGround = isGrounded();
        Debug.Log("Grounded = " + OnGround);
    }

    //FixedUpdate is called once per TimeStamp (computer time) and is the correct way to move
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        handleLayers();
        handleMovement(horizontal);
        flip(horizontal);
    }

    private void handleMovement(float horizontal)
    {
        if (NinjaBody.velocity.y < 0)
        {
            ninjaAnimator.SetBool("land",true);
        }
        if (OnGround)
        {
            Debug.Log("On ground");
            NinjaBody.velocity = new Vector2(horizontal * movementSpeed, 0);
        }
        if (Jump && NinjaBody.velocity.y == 0)
        {
            NinjaBody.AddForce(new Vector2(0, jumpForce));
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ninjaAnimator.SetTrigger("jump");
        }
    }

    private bool isGrounded()
    {
        if(NinjaBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] coliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatsGround);

                for(int i=0; i<coliders.Length; i++)
                {
                    if(coliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void handleLayers()
    {
        if (!OnGround)
        {
            ninjaAnimator.SetLayerWeight(1, 1);
        }
        else{
            ninjaAnimator.SetLayerWeight(0, 1);
        }
    }
}
