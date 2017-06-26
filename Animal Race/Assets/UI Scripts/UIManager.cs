using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private float bestFitnessEver;
    private float bestFitnessNow;
    private float worstFitnessNow;
    private int generation;
    private int bestGeneration;

    [SerializeField]
    private GameObject[] collection;
    [SerializeField]
    private Camera[] cam;
    [SerializeField]
    private Button[] btns;

    private GA ga;
    private Entity ne;
    private Agent[] agent;

    private Image img;

    public void Start()
    {
        ne = collection[0].GetComponentInChildren<Entity>();
        ga = ne.getGa();
        agent = collection[0].GetComponentsInChildren<Agent>();
        bestFitnessEver = 0.0f;
        bestFitnessNow = 0.0f;
        generation = -1;
        worstFitnessNow = 0.0f;
        bestGeneration = 0;
    }

    public void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.Label(new Rect(5, 0, 500, 20), "Best Fitness Ever: " + bestFitnessEver + " Generation: "+bestGeneration);
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
                    bestGeneration = ga.getGeneration();
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
