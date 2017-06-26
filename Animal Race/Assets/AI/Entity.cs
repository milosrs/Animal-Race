using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    private GA genAlg;

    [SerializeField]
    private GameObject[] players;

    private List<Agent> agents;
    private float fittest;

	// Use this for initialization
	void Start () {
        agents = new List<Agent>();
        genAlg = new GA();
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 14;
        genAlg.GenerateNewGeneration(players.Length, totalWeights);


        //kad necemo da ucitamo onda je true
        if (true)
        {
            for (int i = 0; i < players.Length; i++)
            {
                NeuralNetwork nn = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
                Agent a = players[i].GetComponent<Agent>();
                a.AttachNet(nn);
                nn.toGenome(genAlg.getGenomeAt(0));
                agents.Add(a);
            }
        }
        else
        {
            genAlg.Load();
            for (int i = 0; i < players.Length; i++)
            {
                NeuralNetwork nn = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
                Agent a = players[i].GetComponent<Agent>();
                nn.fromGenome(genAlg.getGenomeAt(i));
                a.AttachNet(nn);
                //nn.toGenome(genAlg.getGenomeAt(0));
                agents.Add(a);
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GenerationKill())
        {
            //dodaj fitnese u GA
            //sredi novu generaciju
            //promeni tezine neuronskih u agentima 
            for (int i = 0; i < agents.Count; i++)
            {
                genAlg.getGenomeAt(i).fitness = agents[i].GetFitness();
            }

            //kolko sam skontao ne smeta mu sto svaku generaciju serijalizuje tj ne radi nesto puno sporije
            genAlg.Save();
            //stvori novu generaciju
            genAlg.BreedNewGeneration();

            //nakon kreiranja nove generacije uzmi nove tezine iz GA i zameni ih u neuronskim mrezama agenata
            //Ponovo pokreni agente
            for (int i=0; i<agents.Count; i++)
            {
                agents[i].getNeuralNetwork().fromGenome(genAlg.getGenomeAt(i));
                agents[i].StartAgain();
            }
        }
	}

    public bool GenerationKill()
    {
        bool ret = true;
        for (int i = 0; i < agents.Count; i++)
        {
            ret = ret && agents[i].GetFailed();
        }
        return ret;
    }

    public GA getGa()
    {
        return genAlg;
    }
}
