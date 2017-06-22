using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour{

    //Da li je ninja pao, delta predjenog puta, predjeni put, vreme koje je ziv.
    private bool failed = false;
    private float distanceDelta, distance, timeAlive;
    
    //Neuronska mreza, i vektor2 (Za senzore)
    private NeuralNetwork nn;
    private Vector3 left, right, jump, down30, down50;

    //Gde su nasi senzori detektovali koliziju?
    private Vector3 hitLeft, hitRight, hitUp, hit30, hit50;
    private Vector2 origin;

    //Sta su nasi senzori, a sta je igrac
    public GameObject[] sensors;
    public GameObject player;

    //Igracevo telo
    private Rigidbody2D playerBody;

    private void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
        Vector2 origin = playerBody.position + Vector2.up*0.2f;

        sensors = new GameObject[5];
        failed = false;
        nn = new NeuralNetwork(5, 5, new int[] { 4, 4 }, 3, 3);

        left = new Vector3(-origin.x, origin.y, 0);
        right = new Vector3(origin.x, origin.y, 0);
        Rigidbody2D afterJump = new Rigidbody2D();
        jump = new Vector3(origin.x, origin.y+4, 0);
        down30 = new Vector3(origin.x, origin.y);
        down50 = new Vector3(origin.x, origin.y);
    }

    private void FixedUpdate()
    {
        castSensors();
        if (!failed)
        {
            timeAlive += Time.deltaTime;    //Kada ovo dodje do 200, failed = true
        }
        if (timeAlive >= 200)
        {
            failed = false;                 //Yes
        }
    }

    private void castSensors()
    {
        //Need fixing
        Physics2D.Linecast(playerBody.position + Vector2.up * 0.2f, left);
        Debug.DrawLine(playerBody.position + Vector2.up * 0.2f, left, Color.yellow);

        Physics2D.Linecast(playerBody.position + Vector2.up * 0.2f, right);
        Debug.DrawLine(playerBody.position + Vector2.up * 0.2f, left, Color.blue);

        Physics2D.Linecast(playerBody.position + Vector2.up * 0.2f, jump);
        Debug.DrawLine(playerBody.position + Vector2.up * 0.2f, left, Color.yellow);

        Physics2D.Linecast(playerBody.position + Vector2.up * 0.2f, down30);
        Debug.DrawLine(playerBody.position + Vector2.up * 0.2f, left, Color.yellow);

        Physics2D.Linecast(playerBody.position + Vector2.up * 0.2f, down50);
        Debug.DrawLine(playerBody.position + Vector2.up * 0.2f, left, Color.yellow);
    }


    /*public Agent()
    {
        NeuralNetwork nn = new NeuralNetwork(5, 5, new int[] { 4, 4 }, 3, 3);

        List<float> inputValues = new List<float>();

        List<float> result = nn.Calculate(inputValues);

        foreach(float f in result)
        {
            Debug.Log(f);
        }
    }*/



}
