using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour{

    //Da li je char pao, delta predjenog puta, predjeni put, vreme koje je ziv.
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

    //Igraceve komande
    private float commandL, commandR, commandU;
    //Komande
    private float u, l, r, ur, dr, ddr;

    private float distanceCoeficient = 2.0f;
    private float timeAliveCoeficient = 1.0f;

    private Vector2 startPosition;

    private float fitness;

    // Use this for initialization
    void Awake()
    {
        hits = new List<RaycastHit2D>();
        distances = new List<float>();
        failed = false;

        startPosition = transform.position; //pocetna pozicija karaktera da bi ga nakon padanja mogli vratiti na istu pocetnu poziciju 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distances.Clear();
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
            if (player.gameObject.name.Contains("Ninja"))
            {
                ((Ninja)player.gameObject.GetComponent<Ninja>()).setHorizontal(0f);
            }
            else if (player.gameObject.name.Contains("Dog"))
            {
                ((DoggoScript)player.gameObject.GetComponent<DoggoScript>()).setHorizontal(0f);
            }
            else if (player.gameObject.name.Contains("Squir"))
            {
                ((SquirrelScript)player.gameObject.GetComponent<SquirrelScript>()).setHorizontal(0f);
            }
            else if (player.gameObject.name.Contains("Dragon"))
            {
                ((DragonScript)player.gameObject.GetComponent<DragonScript>()).setHorizontal(0f);
            }
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


            if (player.gameObject.name.Contains("Ninja"))
            {
                this.gameObject.GetComponent<Ninja>().commandMe(commandL, commandR, commandU);
            }
            else if (player.gameObject.name.Contains("Dog"))
            {
                this.gameObject.GetComponent<DoggoScript>().commandMe(commandL, commandR, commandU);
            }
            else if (player.gameObject.name.Contains("Squir"))
            {
                this.gameObject.GetComponent<SquirrelScript>().commandMe(commandL, commandR, commandU);
            }
            else if (player.gameObject.name.Contains("Dragon"))
            {
                this.gameObject.GetComponent<DragonScript>().commandMe(commandL, commandR, commandU);
            }

            CalculateFitness();
        }

        if (timeAlive >= 35)
        {
            failed = true;
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
        //sracunaj fitness
        CalculateFitness();
        gameObject.SetActive(false); //iskljuci lika sa mape dok se ne sracuna fitnes i dok ne dobije bolju neuronsku
    }

    public void StartAgain() //kada mu promenimo mrezu,vratimo ga na pocetak izo ovog stanja
    {
        //respawnuj karaktera na pocetak
        //pusti ga da radi sta inace radi
        transform.position = startPosition;
        fitness = 0.0f;
        gameObject.SetActive(true);
        timeAlive = 0;
    }

    public bool GetFailed()
    {
        return failed;
    }

    public void SetFail(bool fail)
    {
        failed = fail;
    }

    /// <summary>
    /// Fitness mora da se racuna u momentu kad karakter hoda jer cemo crtati
    /// grafove, i kamera prati onog koji ima najbolji fitnes
    /// </summary>
    public void CalculateFitness()
    {
        //Fitnes ne sme da bude menhetn rastojanje, jer ce tako nindze misliti da smeju da idu u levo i nikada nece nauciti da idu u desno.
        //Y nam ne igra nikakvu ulogu u fitnesu, nego broj preskocenih prepreka i to ako stignemo da implementiramo.
        ///menhetn rastojanje od pocetne tacke pa do tacke gde je pao
        float dist = transform.position.x;

        /*//menhetn rastojanje od pocetne tacke pa do tacke gde je pao
        float dist = Mathf.Abs(startPosition.x - transform.position.x) + Mathf.Abs(startPosition.y - transform.position.y);
        */
        fitness = dist;
    }

    public float GetFitness()
    {
        return fitness;
    }

    public NeuralNetwork getNeuralNetwork()
    {
        return nn;
    }

    public Vector2 getStartPosition()
    {
        return this.startPosition;
    }
}
