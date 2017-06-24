using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    private Agent testAgent;                        //Trenutni agent koji je aktivan
    private List<Agent> agents;                     //Svi agenti
    public float bestFitness;
    private float currentTimer;
    private float currAgentFitness;

    //public List<NeuralNetwork> neuralNets;        Ako budemo morali da imamo vise instanci
    private NeuralNetwork neuralNet;

    public GA genAlg;

    private Vector3 defaultpos;
    private Quaternion defaultrot;
    private int numberOfSprites = 8;

    /// <summary>
    /// totalWeights = br senzora * br ulaznih neurona + skriveni sloj * br_izlaza iz prethodnog +
    /// + skriveni sloj * br_izlaza iz prethodnog + broj izlaznih neurona * broj izlaza iz prethodnog
    /// + broj bias tezina (suma svih neurona)
    /// Svaki sledeci sloj ima onoliko ulaza u svaki neuron koliko ima izlaza iz prethodnog sloja
    /// </summary>
    private void Start()
    {
        genAlg = new GA();
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 17;
        genAlg.GenerateNewGeneration(numberOfSprites, totalWeights);
        bestFitness = 0.0f;
        currAgentFitness = 0.0f;

        neuralNet = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        Genome g = genAlg.getNextGenome();
        neuralNet.fromGenome(g);
        testAgent = gameObject.GetComponent<Agent>();
        testAgent.attachNet(neuralNet);

        /*neuralNets = new List<NeuralNetwork>();
        for(int i=0; i<numberOfSprites; i++)
        {
            neuralNets.Add(new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3));      OVO JE ZA VISE INSTANCI KARAKTERA, NE RADI!!!!!
            Genome g = new Genome();
            neuralNets[i].fromGenome(g);
            Agent a = this.gameObject.GetComponent<Agent>();
            a.attachNet(neuralNets[i]);
        }*/

        defaultpos = transform.position;
        defaultrot = transform.rotation;
    }

    private void Update()
    {
        if (testAgent.getFail())
        {
            if (genAlg.getCurrentGenomeIndex() == 15-1)
            {
                EvolveGenomes();
                return;
            }
        }
        if (gameObject.name == "Ninja")
        {
            currAgentFitness = Ninja.Instance.getDistance();
        }
        else if(gameObject.name == "Doggo")
        {
            currAgentFitness = DoggoScript.Instance.getDistance();
        }
        else if(gameObject.name == "Squirell")
        {
            currAgentFitness = SquirrelScript.Instance.getDistance();
        }
        else if(gameObject.name == "Dragon")
        {
            currAgentFitness = DragonScript.Instance.getDistance();
        }
        
        if(currAgentFitness > bestFitness)
        {
            bestFitness = currAgentFitness;
        }
    }

    public void EvolveGenomes()
    {
        genAlg.BreedNewGeneration();
        NextTestSubject();
    }

    public void NextTestSubject()
    {
        genAlg.setGenomeFitness(currAgentFitness, genAlg.getCurrentGenomeIndex() - 1);
        currAgentFitness = 0.0f;
        Genome g = genAlg.getNextGenome();

        neuralNet.fromGenome(g);

        transform.position = defaultpos;
        transform.rotation = defaultrot;

        testAgent.attachNet(neuralNet);
        testAgent.setFail(false);
    }
}
