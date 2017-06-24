using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoScript : MonoBehaviour {

    //DOGGOS RIGID BODY AND ANIMATOR
    private Animator doggoAnimator;
    public Rigidbody2D DoggoBody { get; set; }
    private Agent agent;

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

    /*[SerializeField]
    private GameObject kunai;           //This will be Doggo special ability
    */

    //Special ability
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMulti = 2f;

    //Properties
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool Throw { get; set; }
    

    //Singleton
    private static DoggoScript instance;
    public static DoggoScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DoggoScript>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        facingRight = true;
        DoggoBody = GetComponent<Rigidbody2D>();
        doggoAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame, and it is not the correct way to move a character
    void Update()
    {
        handleInput();
        OnGround = IsGrounded();
    }


    //FixedUpdate is called once per TimeStamp (computer time) and is the correct way to move
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        handleLayers();
        handleMovement(horizontal);
        flip(horizontal);
        if (DoggoBody.velocity.y < 0)
        {
            DoggoBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (DoggoBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            DoggoBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
    }

    private void handleMovement(float horizontal)
    {
        if (DoggoBody.velocity.y < 0)
        {
            doggoAnimator.SetBool("land", true);
        }

        DoggoBody.velocity = new Vector2(horizontal * movementSpeed, DoggoBody.velocity.y);

        if (Jump && OnGround)
        {
            DoggoBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
        doggoAnimator.SetFloat("speed", Mathf.Abs(horizontal));
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
            Jump = true;
            doggoAnimator.SetTrigger("jump");
        }
        /*if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            doggoAnimator.SetTrigger("throw");
            //throwKunai(0);                CALL SPECIAL ABILITY
        }*/
    }

    private bool IsGrounded()
    {
        if (DoggoBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
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
            doggoAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            doggoAnimator.SetLayerWeight(0, 1);
        }
    }

    /*private void throwKunai(int val)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(kunai, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<KunaiScript>().init(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(kunai, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<KunaiScript>().init(Vector2.left);
        }
        // THIS WILL BE A METHOD FOR DOGS SPECIAL ABILITY
    }*/

    public void commandMe(float l, float r, float u)
    {
        if (l > r && l > u)
        {
            Debug.Log("Dog should move left");
            l = -1;
            flip(l);
            handleMovement(l);
        }
        else if (r > l && r > u)
        {
            Debug.Log("Dog should move right");
            flip(r);
            r = 1;
            handleMovement(r);
        }
        else if (u > l && u > r && DoggoBody.velocity.y == 0)
        {
            Debug.Log("Dog should jump");
            Jump = true;
            doggoAnimator.SetTrigger("jump");
        }
    }

    public Agent getAgent()
    {
        return agent;
    }

}
