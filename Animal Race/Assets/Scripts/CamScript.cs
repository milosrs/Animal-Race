using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    private NinjaEntity entity;

    private List<Ninja> ninjas;
    private List<DoggoScript> dogs;
    private List<SquirrelScript> squirrels;
    private List<DragonScript> dragons;

    private Rigidbody2D[] bodies;

    private void Start()
    {
        entity = player.GetComponentInChildren<NinjaEntity>();
    }

    void Awake () {
        if (player.name.Contains("Ninja"))
        {
            ninjas = new List<Ninja>();
            Ninja[] scripts = player.GetComponentsInChildren<Ninja>();
            bodies = new Rigidbody2D[scripts.Length];

            if (scripts != null)
            {
                int i = 0;
                foreach (Ninja n in scripts)
                {
                    ninjas.Add(n);
                    bodies[i] = n.GetComponent<Rigidbody2D>();
                    i++;
                }
            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 7, 0, -10);
        }
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = new Vector3(player.transform.position.x + 7, 0, -10);
        if (player.name.Contains("Ninja"))
        {
            if (!entity.GenerationKill())
            {
                float maxFit = 0.0f;
                int maxInd = -1;
                int i = 0;
                foreach (Ninja n in ninjas)
                {
                    if (n.getAgent().GetFitness() > maxFit && n.getAgent().GetFailed() != true && n.isActiveAndEnabled)
                    {
                        maxFit = n.getAgent().GetFitness();
                        maxInd = i;
                    }
                    i++;
                }
                if (maxInd > -1 && ninjas[maxInd].isActiveAndEnabled)
                    transform.position = new Vector3(bodies[maxInd].position.x + 7, 0, -10);
                else if (ninjas.Count > 0)
                    transform.position = new Vector3(ninjas[0].getAgent().getStartPosition().x, 0, -10);
            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 7, 0, -10);
        }
    }
}
