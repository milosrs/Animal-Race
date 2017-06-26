using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public GA genAlg;

    [SerializeField]
    private GameObject player;

    private Agent agent;

    /// <summary>
    /// totalWeights = br senzora * br ulaznih neurona + skriveni sloj * br_izlaza iz prethodnog +
    /// + skriveni sloj * br_izlaza iz prethodnog + broj izlaznih neurona * broj izlaza iz prethodnog
    /// + broj bias tezina (suma svih neurona)
    /// Svaki sledeci sloj ima onoliko ulaza u svaki neuron koliko ima izlaza iz prethodnog sloja
    /// </summary>
    private void Start()
    {
        genAlg = new GA();
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 14;
        genAlg.GenerateNewGeneration(1, totalWeights);

        NeuralNetwork neuralNet = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        
        agent = player.GetComponent<Agent>();
        agent.AttachNet(neuralNet);
        neuralNet.toGenome(genAlg.getGenomeAt(0));
    }

    private void Update()
    {
        if (agent.GetFailed())
        {
            genAlg.getGenomeAt(0);
            genAlg.BreedNewGeneration();
            agent.getNeuralNetwork().fromGenome(genAlg.getGenomeAt(0));
        }
        else
        {

        }
    }
}
