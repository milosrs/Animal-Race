using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class textScript : MonoBehaviour {

    private float bestFitnessEver;
    private float bestFitnessNow;
    private float worstFitnessNow;
    private int generation;

    [SerializeField]
    private GameObject collection;
    [SerializeField]
    private Camera cam;

    private GA ga;
    private NinjaEntity ne;
    private Agent[] agent;

    public void Start()
    {
        ne = collection.GetComponentInChildren<NinjaEntity>();
        ga = ne.getGa();
        agent = collection.GetComponentsInChildren<Agent>();
        bestFitnessEver = 0.0f;
        bestFitnessNow = 0.0f;
        generation = -1;
        worstFitnessNow = 0.0f;
    }

    public void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.Label(new Rect(5, 0, 200, 20), "Best Fitness Ever: " + bestFitnessEver);
        GUI.Label(new Rect(5, 30, 200, 20), "Best in generation: " + bestFitnessNow);
        GUI.Label(new Rect(5, 60, 200, 20), "Worst in generation: " + worstFitnessNow);
        GUI.Label(new Rect(5, 90 + 20, 200, 20), "Generation: " + generation);
    }

    public void FixedUpdate()
    {
        if (ga != null)
        {
            if(generation < ga.getGeneration())
            {
                bestFitnessNow = 0.0f;
                worstFitnessNow = agent[0].GetFitness();
                generation = ga.getGeneration();
            }
            for (int i = 0; i < agent.Length; i++)
            {
                if (agent[i].GetFitness() > bestFitnessEver)
                {
                    bestFitnessEver = agent[i].GetFitness();
                }
            }
            for(int i=0; i < agent.Length; i++)
            {
                if(agent[i].GetFitness() > bestFitnessNow)
                {
                    bestFitnessNow = agent[i].GetFitness();
                }
            }
            for(int i=0; i<agent.Length; i++)
            {
                if(agent[i].GetFitness() < worstFitnessNow)
                {
                    worstFitnessNow = agent[i].GetFitness();
                }
            }
        }
        else
        {
            ga = ne.getGa();
        }
    }

}
