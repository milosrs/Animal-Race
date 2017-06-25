using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEntity : MonoBehaviour {

    private GA genAlg;

    [SerializeField]
    private GameObject[] players;

    private NinjaAgent1 agent1;
    private NinjaAgent2 agent2;
    private NinjaAgent3 agent3;
    private NinjaAgent4 agent4;
    private NinjaAgent5 agent5;
    private NinjaAgent6 agent6;
    private NinjaAgent7 agent7;
    private NinjaAgent1 agent8;





	// Use this for initialization
	void Start () {
        genAlg = new GA();
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 14;
        genAlg.GenerateNewGeneration(players.Length, totalWeights);

        NeuralNetwork nn1 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent1 = players[0].GetComponent<NinjaAgent1>();
        agent1.AttachNet(nn1);
        nn1.toGenome(genAlg.getGenomeAt(0));

        NeuralNetwork nn2 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent2 = players[1].GetComponent<NinjaAgent2>();
        agent2.AttachNet(nn2);
        nn2.toGenome(genAlg.getGenomeAt(1));

        NeuralNetwork nn3 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent3 = players[2].GetComponent<NinjaAgent3>();
        agent3.AttachNet(nn3);
        nn3.toGenome(genAlg.getGenomeAt(2));

        NeuralNetwork nn4 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent4 = players[3].GetComponent<NinjaAgent4>();
        agent4.AttachNet(nn4);
        nn4.toGenome(genAlg.getGenomeAt(3));

        NeuralNetwork nn5 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent5 = players[4].GetComponent<NinjaAgent5>();
        agent5.AttachNet(nn5);
        nn5.toGenome(genAlg.getGenomeAt(4));

        NeuralNetwork nn6 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent6 = players[5].GetComponent<NinjaAgent6>();
        agent6.AttachNet(nn6);
        nn6.toGenome(genAlg.getGenomeAt(5));

        NeuralNetwork nn7 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent7 = players[6].GetComponent<NinjaAgent7>();
        agent7.AttachNet(nn7);
        nn7.toGenome(genAlg.getGenomeAt(6));

        NeuralNetwork nn8 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent8 = players[7].GetComponent<NinjaAgent1>();
        agent8.AttachNet(nn8);
        nn8.toGenome(genAlg.getGenomeAt(7));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GenerationKill())
        {
            //dodaj fitnese u GA
            //sredi novu generaciju
            //promeni tezine neuronskih u agentima 
            genAlg.getGenomeAt(0).fitness = agent1.GetFitness();
            genAlg.getGenomeAt(1).fitness = agent2.GetFitness();
            genAlg.getGenomeAt(2).fitness = agent3.GetFitness();
            genAlg.getGenomeAt(3).fitness = agent4.GetFitness();

            genAlg.getGenomeAt(4).fitness = agent5.GetFitness();
            genAlg.getGenomeAt(5).fitness = agent6.GetFitness();
            genAlg.getGenomeAt(6).fitness = agent7.GetFitness();
            genAlg.getGenomeAt(7).fitness = agent8.GetFitness();

            genAlg.BreedNewGeneration();

            //nakon kreiranja nove generacije uzmi nove tezine iz GA i zameni ih u neuronskim mrezama agenata
            agent1.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(0));
            agent2.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(1));
            agent3.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(2));
            agent4.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(3));

            agent5.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(4));
            agent6.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(5));
            agent7.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(6));
            agent8.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(7));
            
            //pokreni sledecu generaciju
            agent1.StartAgain();
            agent2.StartAgain();
            agent3.StartAgain();
            agent4.StartAgain();

            agent5.StartAgain();
            agent6.StartAgain();
            agent7.StartAgain();
            agent8.StartAgain();
        }
	}

    private bool GenerationKill()
    {
        return agent1.GetFailed() && agent2.GetFailed() && agent3.GetFailed() && agent4.GetFailed() && agent5.GetFailed() && agent6.GetFailed() && agent7.GetFailed() && agent8.GetFailed();
    }
}
