using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    private Entity entity;

    private List<Ninja> ninjas;
    private List<DoggoScript> dogs;
    private List<SquirrelScript> squirrels;
    private List<DragonScript> dragons;

    private Rigidbody2D[] bodies;

    private void Start()
    {
        entity = player.GetComponentInChildren<Entity>();
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
        else if (player.name.Contains("Dog"))
        {
            dogs = new List<DoggoScript>();
            DoggoScript[] scripts = player.GetComponentsInChildren<DoggoScript>();
            bodies = new Rigidbody2D[scripts.Length];

            if (scripts != null)
            {
                int i = 0;
                foreach (DoggoScript n in scripts)
                {
                    dogs.Add(n);
                    bodies[i] = n.GetComponent<Rigidbody2D>();
                    i++;
                }
            }
        }
        else if (player.name.Contains("Squi"))
        {
            squirrels = new List<SquirrelScript>();
            SquirrelScript[] scripts = player.GetComponentsInChildren<SquirrelScript>();
            bodies = new Rigidbody2D[scripts.Length];

            if (scripts != null)
            {
                int i = 0;
                foreach (SquirrelScript n in scripts)
                {
                    squirrels.Add(n);
                    bodies[i] = n.GetComponent<Rigidbody2D>();
                    i++;
                }
            }
        }
        else if (player.name.Contains("Dragon"))
        {
            dragons = new List<DragonScript>();
            DragonScript[] scripts = player.GetComponentsInChildren<DragonScript>();
            bodies = new Rigidbody2D[scripts.Length];

            if (scripts != null)
            {
                int i = 0;
                foreach (DragonScript n in scripts)
                {
                    dragons.Add(n);
                    bodies[i] = n.GetComponent<Rigidbody2D>();
                    i++;
                }
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
        else if (player.name.Contains("Dog"))
        {
            if (!entity.GenerationKill())
            {
                float maxFit = 0.0f;
                int maxInd = -1;
                int i = 0;
                foreach (DoggoScript n in dogs)
                {
                    if (n.getAgent().GetFitness() > maxFit && n.getAgent().GetFailed() != true && n.isActiveAndEnabled)
                    {
                        maxFit = n.getAgent().GetFitness();
                        maxInd = i;
                    }
                    i++;
                }
                if (maxInd > -1 && dogs[maxInd].isActiveAndEnabled)
                    transform.position = new Vector3(bodies[maxInd].position.x + 7, 0, -10);
                else if (dogs.Count > 0)
                    transform.position = new Vector3(dogs[0].getAgent().getStartPosition().x, 0, -10);
            }
        }
        else if (player.name.Contains("Squi"))
        {
            if (!entity.GenerationKill())
            {
                float maxFit = 0.0f;
                int maxInd = -1;
                int i = 0;
                foreach (SquirrelScript n in squirrels)
                {
                    if (n.getAgent().GetFitness() > maxFit && n.getAgent().GetFailed() != true && n.isActiveAndEnabled)
                    {
                        maxFit = n.getAgent().GetFitness();
                        maxInd = i;
                    }
                    i++;
                }
                if (maxInd > -1 && squirrels[maxInd].isActiveAndEnabled)
                    transform.position = new Vector3(bodies[maxInd].position.x + 7, 0, -10);
                else if (squirrels.Count > 0)
                    transform.position = new Vector3(squirrels[0].getAgent().getStartPosition().x, 0, -10);
            }
        }
        else if (player.name.Contains("Dragon"))
        {
            if (!entity.GenerationKill())
            {
                float maxFit = 0.0f;
                int maxInd = -1;
                int i = 0;
                foreach (DragonScript n in dragons)
                {
                    if (n.getAgent().GetFitness() > maxFit && n.getAgent().GetFailed() != true && n.isActiveAndEnabled)
                    {
                        maxFit = n.getAgent().GetFitness();
                        maxInd = i;
                    }
                    i++;
                }
                if (maxInd > -1 && dragons[maxInd].isActiveAndEnabled)
                    transform.position = new Vector3(bodies[maxInd].position.x + 7, 0, -10);
                else if (dragons.Count > 0)
                    transform.position = new Vector3(dragons[0].getAgent().getStartPosition().x, 0, -10);
            }
        }
    }
}
