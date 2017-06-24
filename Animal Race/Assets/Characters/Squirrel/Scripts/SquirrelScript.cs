using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelScript : MonoBehaviour {

    public Rigidbody2D myRigidBody { get; set; }
	private Animator myAnimator;
    private Agent agent;
    private float distance;

	[SerializeField]
	private float movementSpeed;
	private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMulti = 2f;

    public bool isGrounded { get; set; }
    public bool jump { get; set; }

	[SerializeField]
	private bool airControl;

    private static SquirrelScript instance;
    public static SquirrelScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SquirrelScript>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
        isGrounded = IsGrounded();
    }


	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal3");
        HandleLayers();
        HandleMovement(horizontal);
        Flip(horizontal);
        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump3"))
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
	}

	private void HandleMovement(float horizontal)
    {
        if (myRigidBody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);

        if (isGrounded && jump)
        {
            myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //myRigidBody.velocity = new Vector2 (horizontal * movementSpeed, myRigidBody.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void Flip(float horizontal)
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

	private bool IsGrounded(){
		if (myRigidBody.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						return true;	
					}
				}
			}
		}
		return false;
	}

	private void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.B))
        {
            jump = true;
            myAnimator.SetTrigger("jump");
        }
	}

	private void ResetValues(){
		jump = false;
	}

	private void HandleLayers(){
		if (!isGrounded) {
			myAnimator.SetLayerWeight (1, 1);
		} else {
			myAnimator.SetLayerWeight (1, 0);
		}
	}

    public void commandMe(float l, float r, float u)
    {
        if (l > r && l > u)
        {
            Debug.Log("Squirrel should move left");
            l = -1;
            Flip(l);
            HandleMovement(l);
        }
        else if (r > l && r > u)
        {
            Debug.Log("Squirrel should move right");
            Flip(r);
            r = 1;
            HandleMovement(r);
        }
        else if (u > l && u > r && myRigidBody.velocity.y == 0)
        {
            Debug.Log("Squirrel should jump");
            jump = true;
            myAnimator.SetTrigger("jump");
        }
    }

    public Agent getAgent()
    {
        return agent;
    }

    public float getDistance()
    {
        return distance;
    }

    public void setDistance(float d)
    {
        distance = d;
    }
}
