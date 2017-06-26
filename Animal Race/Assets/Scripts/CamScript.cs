using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    private List<Ninja> ninjas;
    private List<DoggoScript> dogs;
    private List<SquirrelScript> squirrels;
    private List<DragonScript> dragons;

    private Rigidbody2D[] bodies;
    private float yMin = -15f ,yMax = 15f;
    //Eine hack bitte
    private string namez;

	// Use this for initialization
	void Awake () {
        namez = "";
        if (player.name.Contains("Ninja"))
        {
            namez = "Ninja";
            ninjas = new List<Ninja>();
            Ninja[] scripts = player.GetComponentsInChildren<Ninja>();
            bodies = new Rigidbody2D[scripts.Length];

            int i = 0;
            foreach(Ninja n in scripts)
            {
                ninjas.Add(n);
                bodies[i] = n.GetComponent<Rigidbody2D>();
                i++;
            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 7, 0, -10);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (namez.Equals("Ninja"))
        {
            float maxFit = 0.0f;
            int maxInd = -1;
            int i = 0;
            foreach (Ninja n in ninjas)
            {
                if (n.getAgent().GetFitness() > maxFit && n.getAgent().GetFailed()!=true && n.isActiveAndEnabled==true)
                {
                    maxFit = n.getAgent().GetFitness();
                    maxInd = i;
                }
                i++;
            }
            if(maxInd>-1)
             transform.position = new Vector3(bodies[maxInd].position.x + 7, 0, -10);

            Debug.Log("Fittest:" + maxFit);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 7, 0, -10);
        }
    }
}
