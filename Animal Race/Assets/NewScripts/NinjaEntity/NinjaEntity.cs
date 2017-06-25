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




	// Use this for initialization
	void Start () {
        genAlg = new GA();
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 17;
        genAlg.GenerateNewGeneration(players.Length, totalWeights);

        NeuralNetwork nn1 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent1 = players[0].GetComponent<NinjaAgent1>();
        agent1.AttachNet(nn1);
        nn1.toGenome(genAlg.getGenomeAt(0));

        NeuralNetwork nn2 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent2 = players[1].GetComponent<NinjaAgent2>();
        agent2.AttachNet(nn2);
        nn1.toGenome(genAlg.getGenomeAt(1));

        NeuralNetwork nn3 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent3 = players[2].GetComponent<NinjaAgent3>();
        agent3.AttachNet(nn3);
        nn1.toGenome(genAlg.getGenomeAt(2));

        NeuralNetwork nn4 = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        agent4 = players[3].GetComponent<NinjaAgent4>();
        agent4.AttachNet(nn4);
        nn1.toGenome(genAlg.getGenomeAt(3));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GenerationKill())
        {
            //dodaj fitnese u GA
            //sredi novu generaciju
            //promeni tezine neuronskih u agentima 



            //pokreni sledecu generaciju
            agent1.StartAgain();
            agent2.StartAgain();
            agent3.StartAgain();
            agent4.StartAgain();
        }
	}

    private bool GenerationKill()
    {
        return agent1.GetFailed() && agent2.GetFailed() && agent3.GetFailed() && agent4.GetFailed();
    }
}
