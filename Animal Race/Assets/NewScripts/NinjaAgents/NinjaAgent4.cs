using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAgent4 : MonoBehaviour {

    private bool failed;
    private float distanceDelta, distance, timeAlive;

    private NeuralNetwork nn;

    private List<RaycastHit2D> hits;
    private List<float> distances;

    public GameObject[] sensorStart;    //UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWNERRIGHT, LEFT
    public GameObject[] sensorEnd;

    private Rigidbody2D playerBody;
    private float commandL, commandR, commandU;

    private float u, l, r, ur, dr, ddr;

    private Vector3 startPosition;

    // Use this for initialization
    void Awake()
    {
        hits = new List<RaycastHit2D>();
        distances = new List<float>();
        playerBody = GetComponent<Rigidbody2D>();
        failed = false;

        startPosition = transform.position; //pocetna pozicija karaktera da bi ga nakon padanja mogli vratiti na istu pocetnu poziciju 
    }

    // Update is called once per frame
    void Update()
    {

        CastSensors();
        u = distances[0];
        l = distances[1];
        r = distances[2];
        ur = distances[3];
        dr = distances[4];
        ddr = distances[5];

        IsFailed();

        if (failed)
        {
            KillMe();
        }
        else
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

            GetComponent<Ninja4>().CommandMe(commandL, commandR, commandU);
        }

        if (timeAlive >= 20)
        {
            failed = true;
            timeAlive = 0;
        }
    }

    public float Normalise(float i)
    {
        float depth = i / 3.0f;             //Prosecna duzina Ray-a
        return 1 - depth;
    }

    private void CastSensors()
    {
        for (int i = 0; i < sensorEnd.Length; i++)
        {
            RaycastHit2D result = Physics2D.Linecast(new Vector2(sensorStart[i].transform.position.x, sensorStart[i].transform.position.y), new Vector2(sensorEnd[i].transform.position.x, sensorEnd[i].transform.position.y));
            hits.Add(result);
            distances.Add(result.distance);
            Debug.DrawLine(new Vector2(sensorStart[i].transform.position.x, sensorStart[i].transform.position.y), new Vector2(sensorEnd[i].transform.position.x, sensorEnd[i].transform.position.y), Color.green);
        }
    }

    public void AttachNet(NeuralNetwork nn)
    {
        this.nn = nn;
    }

    public void IsFailed()
    {
        if (transform.position.y < -5)
            failed = true;
        else
            failed = false;
    }

    public void KillMe() //kada padne karakter onda ga u ovo stanje stavljamo
    {
        gameObject.SetActive(false);
    }

    public void StartAgain() //kada mu promenimo mrezu,vratimo ga na pocetak izo ovog stanja
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public bool GetFailed()
    {
        return failed;
    }
}
