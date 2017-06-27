using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //NINJA
    private float ninjaBestFit;
    private float ninjaCurBest;
    private float ninjaCurWorst;
    private int ninjaGeneration;
    private int ninjaBestGeneration;

    private float dogBestFit;
    private float dogCurBest;
    private float dogCurWorst;
    private int dogGeneration;
    private int dogBestGeneration;

    private float sqBestFit;
    private float sqCurBest;
    private float sqCurWorst;
    private int sqGeneration;
    private int sqBestGeneration;

    private float dragonBestFit;
    private float dragonCurBest;
    private float dragonCurWorst;
    private int dragonGeneration;
    private int dragonBestGeneration;

    [SerializeField]
    private GameObject collection;
    [SerializeField]
    private Image[] orienters;

    private GA ga;
    private Entity ne;
    private Agent[] agent;
    private Vector2[] orienterPositions;

    private Image img;

    public void Start()
    {
        orienterPositions = new Vector2[2];
        ne = collection.GetComponentInChildren<Entity>();
        ga = ne.getGa();
        agent = collection.GetComponentsInChildren<Agent>();
        ninjaBestFit = 0;
        ninjaCurBest = 0;
        ninjaCurWorst=0;
        ninjaGeneration=0;
        ninjaBestGeneration = 0;

        dogBestFit = 0;
        dogCurBest = 0;
        dogCurWorst = 0;
        dogGeneration = 0;
        dogBestGeneration = 0;

        sqBestFit = 0;
        sqCurBest = 0;
        sqCurWorst = 0;
        sqGeneration = 0;
        sqBestGeneration = 0;

        dragonBestFit = 0;
        dragonCurBest = 0;
        dragonCurWorst = 0;
        dragonGeneration = 0;
        dragonBestGeneration = 0;

        orienterPositions[0] = orienters[0].transform.position;
        orienterPositions[1] = orienters[1].transform.position;
        Debug.Log("Orineter positions: 1:" + orienterPositions[0] + " 2:" + orienterPositions[1]);
    }

    public void OnGUI()
    {
        
        GUI.contentColor = Color.red;

        //NINJA
        if (collection.gameObject.name.Contains("Ninja"))
        {
            GUI.Label(new Rect(5, 0, 500, 20), "Best Fitness Ever: " + ninjaBestFit + " Generation: " + ninjaBestGeneration);
            GUI.Label(new Rect(5, 30, 200, 20), "Best in generation: " + ninjaCurBest);
            GUI.Label(new Rect(5, 60, 200, 20), "Worst in generation: " + ninjaCurWorst);
            GUI.Label(new Rect(5, 90 + 20, 200, 20), "Generation: " + ninjaGeneration);
        }
        else if (collection.gameObject.name.Contains("Dog"))
        {
            GUI.Label(new Rect(orienterPositions[0].x + 5, 0, 500, 20), "Best Fitness Ever: " + dogBestFit + " Generation: " + dogBestGeneration);
            GUI.Label(new Rect(orienterPositions[0].x + 5, 30, 200, 20), "Best in generation: " + dogCurBest);
            GUI.Label(new Rect(orienterPositions[0].x + 5, 60, 200, 20), "Worst in generation: " + dogCurWorst);
            GUI.Label(new Rect(orienterPositions[0].x + 5, 90 + 20, 200, 20), "Generation: " + dogGeneration);
        }
        else if (collection.gameObject.name.Contains("Squir"))
        {
            GUI.Label(new Rect(5, orienterPositions[0].y, 500, 20), "Best Fitness Ever: " + sqBestFit + " Generation: " + sqBestGeneration);
            GUI.Label(new Rect(5, orienterPositions[0].y + 30, 200, 20), "Best in generation: " + sqCurBest);
            GUI.Label(new Rect(5, orienterPositions[0].y + 60, 200, 20), "Worst in generation: " + sqCurWorst);
            GUI.Label(new Rect(5, orienterPositions[0].y + 90 + 20, 200, 20), "Generation: " + sqGeneration);
        }
        else if (collection.gameObject.name.Contains("Dragon"))
        {
            GUI.Label(new Rect(orienterPositions[0].x + 5, orienterPositions[0].y, 500, 20), "Best Fitness Ever: " + dragonBestFit + " Generation: " + dragonBestGeneration);
            GUI.Label(new Rect(orienterPositions[0].x + 5, orienterPositions[0].y + 30, 200, 20), "Best in generation: " + dragonCurBest);
            GUI.Label(new Rect(orienterPositions[0].x + 5, orienterPositions[0].y + 60, 200, 20), "Worst in generation: " + dragonCurWorst);
            GUI.Label(new Rect(orienterPositions[0].x + 5, orienterPositions[0].y + 90 + 20, 200, 20), "Generation: " + dragonGeneration);
        }
        GUI.Label(new Rect(5, orienterPositions[0].y, 20, 20), "HERE");
    }

    public void FixedUpdate()
    {

        Debug.Log("Orineter positions: 1:" + orienterPositions[0] + " 2:" + orienterPositions[1]);
        if (ga != null)
        {
            if (collection.gameObject.name.Contains("Ninja"))
                ninjaControls();
            else if (collection.gameObject.name.Contains("Dog"))
                dogControls();
            else if (collection.gameObject.name.Contains("Squir"))
                squirControls();
            else if (collection.gameObject.name.Contains("Dragon"))
                dragonControls();
        }
        else
        {
            ga = ne.getGa();
        }
    }


    public void ninjaControls()
    {
        if (ninjaGeneration < ga.getGeneration())
        {
            ninjaCurBest = 0.0f;
            ninjaCurWorst = agent[0].GetFitness();
            ninjaGeneration = ga.getGeneration();
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > ninjaBestFit)
            {
                ninjaBestFit = agent[i].GetFitness();
                ninjaBestGeneration = ga.getGeneration();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > ninjaCurBest)
            {
                ninjaCurBest = agent[i].GetFitness();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() < ninjaCurWorst)
            {
                ninjaCurWorst = agent[i].GetFitness();
            }
        }
    }

    public void dogControls()
    {
        if (dogGeneration < ga.getGeneration())
        {
            dogCurBest = 0.0f;
            dogCurWorst = agent[0].GetFitness();
            dogGeneration = ga.getGeneration();
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > dogBestFit)
            {
                dogBestFit = agent[i].GetFitness();
                dogBestGeneration = ga.getGeneration();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > dogCurBest)
            {
                dogCurBest = agent[i].GetFitness();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() < dogCurWorst)
            {
                dogCurWorst = agent[i].GetFitness();
            }
        }
    }

    public void squirControls()
    {
        if (sqGeneration < ga.getGeneration())
        {
            sqCurBest = 0.0f;
            sqCurWorst = agent[0].GetFitness();
            sqGeneration = ga.getGeneration();
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > sqBestFit)
            {
                sqBestFit = agent[i].GetFitness();
                sqBestGeneration = ga.getGeneration();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > sqCurBest)
            {
                sqCurBest = agent[i].GetFitness();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() < sqCurWorst)
            {
                sqCurWorst = agent[i].GetFitness();
            }
        }
    }

    public void dragonControls()
    {
        if (dragonGeneration < ga.getGeneration())
        {
            dragonCurBest = 0.0f;
            dragonCurWorst = agent[0].GetFitness();
            dragonGeneration = ga.getGeneration();
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > dragonBestFit)
            {
                dragonBestFit = agent[i].GetFitness();
                dragonBestGeneration = ga.getGeneration();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() > dragonCurBest)
            {
                dragonCurBest = agent[i].GetFitness();
            }
        }
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].GetFitness() < dragonCurWorst)
            {
                dragonCurWorst = agent[i].GetFitness();
            }
        }
    }
}
