using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour{

    //Da li je ninja pao, delta predjenog puta, predjeni put, vreme koje je ziv.
    private bool failed = false;
    private float distanceDelta, distance, timeAlive;
    
    //Neuronska mreza, i vektor2 (Za senzore)
    private NeuralNetwork nn;

    //Gde su nasi senzori detektovali koliziju?
    private List<RaycastHit2D> hits;
    private List<float> distances;

    //Sta su nasi senzori, a sta je igrac
    public GameObject[] sensorStart;    //UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWNERRIGHT, LEFT
    public GameObject[] sensorEnd;
    public GameObject player;

    //Igracevo telo
    private Rigidbody2D playerBody;
    private float commandL, commandR, commandU;
    //Komande
    private float u, l, r, ur, dr, ddr;


    private Vector3 startPosition;

    private void Awake()
    {
        hits = new List<RaycastHit2D>();
        distances = new List<float>();
        playerBody = player.GetComponent<Rigidbody2D>();
        failed = false;
        startPosition = player.transform.position;
    }
    
    private void FixedUpdate()
    {
        isFailed();
        castSensors();

        u = distances[0];
        l = distances[1];
        r = distances[2];
        ur = distances[3];
        dr = distances[4];
        ddr = distances[5];

        if (!failed)
        {
            timeAlive += Time.deltaTime;    //Kada ovo dodje do 200, failed = true
            List<float> inputs = new List<float>();
            inputs.Add(Normalise(u));
            inputs.Add(Normalise(l));
            inputs.Add(Normalise(r));
            inputs.Add(Normalise(ur));
            inputs.Add(Normalise(dr));
            inputs.Add(Normalise(ddr));

            nn.setInput(inputs);
            nn.refreshNetwork();

            commandL = nn.getOutput(0);
            commandR = nn.getOutput(1);
            commandU = nn.getOutput(2);

            player.GetComponent<Ninja>().commandMe(commandL, commandR, commandU);
        }
        if (timeAlive >= 20)
        {
            failed = true;                 //Yes
            timeAlive = 0;
        }
        Debug.Log("Time alive = " + timeAlive);
        
    }

    public float Normalise(float i)
    {
        float depth = i / 3.0f;             //Prosecna duzina Ray-a
        return 1 - depth;
    }

    private void castSensors()
    {
        for(int i=0; i<sensorEnd.Length; i++)
        {
            RaycastHit2D result = Physics2D.Linecast(new Vector2(sensorStart[i].transform.position.x, sensorStart[i].transform.position.y), new Vector2(sensorEnd[i].transform.position.x, sensorEnd[i].transform.position.y));
            hits.Add(result);
            distances.Add(result.distance);
            Debug.DrawLine(new Vector2(sensorStart[i].transform.position.x, sensorStart[i].transform.position.y), new Vector2(sensorEnd[i].transform.position.x, sensorEnd[i].transform.position.y), Color.green);
        }
    }

    public void attachNet(NeuralNetwork nn)
    {
        this.nn = nn;
    }

    public bool getFail()
    {
        return failed;
    }

    public void setFail(bool f)
    {
        failed = f;
    }

    public void ClearFailure()
    {
        failed = false;
        Ninja.Instance.setDistance(0.0f);
    }

    public void isFailed()
    {
        if (player.transform.localPosition.y < -5)
        {
            failed = true;
            playerBody.gravityScale = 0;

            player.transform.localPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);

        
        }
    }
}
