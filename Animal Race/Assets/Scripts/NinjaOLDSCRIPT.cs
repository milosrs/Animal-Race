﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaOLDSCRIPT : MonoBehaviour
{

    //NINJAS RIGID BODY AND ANIMATOR
    private Animator ninjaAnimator;
    public Rigidbody2D NinjaBody { get; set; }
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
    [SerializeField]
    private GameObject kunai;

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

    //How far did this character get?
    private float distance;
    private float horizontal;

    //Background collision ignorer
    public GameObject bg1, bg2;

    void Start()
    {
        horizontal = 0;
        agent = GetComponent<Agent>();
        facingRight = true;
        NinjaBody = GetComponent<Rigidbody2D>();
        ninjaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame, and it is not the correct way to move a character
    void Update()
    {
        OnGround = isGrounded();
    }

    //FixedUpdate is called once per TimeStamp (computer time) and is the correct way to move
    private void FixedUpdate()
    {
        if (this.isActiveAndEnabled)
        {
            handleLayers();
            handleMovement(horizontal);
            flip(horizontal);
            if (NinjaBody.velocity.y < 0)
            {
                NinjaBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (NinjaBody.velocity.y > 0/* && !Input.GetButton("Jump")*/)
            {
                NinjaBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
            }
        }
    }

    private void handleMovement(float horizontal)
    {
        if (NinjaBody.velocity.y < 0)
        {
            ninjaAnimator.SetBool("land", true);
        }

        NinjaBody.velocity = new Vector2(horizontal * movementSpeed, NinjaBody.velocity.y);

        if (Jump && OnGround)
        {
            NinjaBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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
            Jump = true;
            ninjaAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ninjaAnimator.SetTrigger("throw");
            throwKunai(0);
        }
    }

    private bool isGrounded()
    {
        if (NinjaBody.velocity.y <= 0)
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

    private void handleLayers()
    {
        if (!OnGround)
        {
            ninjaAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            ninjaAnimator.SetLayerWeight(0, 1);
        }
    }

    private void throwKunai(int val)
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
    }

    public void commandMe(float l, float r, float u)
    {
        if (l > r)
        {
            horizontal = -l;
        }
        else
        {
            horizontal = r;
        }
        if (u > l && u > r && NinjaBody.velocity.y == 0)
        {
            NinjaBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            ninjaAnimator.SetTrigger("jump");
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
    }
}