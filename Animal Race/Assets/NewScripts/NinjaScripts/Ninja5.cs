using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja5 : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Agent agent;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatsGround;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMulti = 2f;

    private bool jump;
    private bool onGround;

    private float distance;
    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleAnimation();
        onGround = IsGrounded();
    }

    private void HandleAnimation()
    {
        if (myRigidBody.velocity.y < 0)
            myAnimator.SetBool("land", true);

        //if (myRigidBody.velocity.x > 0 && ) //ovo radi flip 
        {

        }

    }

    public bool IsGrounded()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] coliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatsGround);

                for (int i = 0; i < coliders.Length; i++)
                {
                    if (coliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void CommandMe(float l, float r, float u)
    {
        if (l > r)
        {
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            myRigidBody.velocity = new Vector2(l * movementSpeed, myRigidBody.velocity.y);

            myAnimator.SetFloat("speed", Mathf.Abs(r));
        }
        else
        {
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            myRigidBody.velocity = new Vector2(r * movementSpeed, myRigidBody.velocity.y);

            myAnimator.SetFloat("speed", Mathf.Abs(l));
        }

        //al ako je skok veci od levo ili desno onda dodaj jos skok na stranu na koju treba skociti
        if (u > l && u > r && myRigidBody.velocity.y == 0)
        {
            myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnimator.SetTrigger("jump");
        }
    }

    private void HandleLayers()
    {
        if (!onGround)
            myAnimator.SetLayerWeight(1, 1);
        else
            myAnimator.SetLayerWeight(0, 1);
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

    public void setAgent(Agent agent)
    {
        this.agent = agent;
    }
}
