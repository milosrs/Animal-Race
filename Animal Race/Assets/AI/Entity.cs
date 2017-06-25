using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    private List<Agent> agents;                     //Svi agenti

    public GA genAlg;

    [SerializeField]
    private GameObject[] players;

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
        int totalWeights = 6 * 6 + 4 * 6 + 4 * 4 + 3 * 4 + 17;
        genAlg.GenerateNewGeneration(players.Length, totalWeights);

        NeuralNetwork neuralNet = new NeuralNetwork(6, 6, new int[] { 4, 4 }, 3, 3);
        //neuralNet.toGenome(ge
        agent = players[0].GetComponent<Agent>();
        agent.attachNet(neuralNet);

        
        
        neuralNet.fromGenome(genAlg.getGenomeAt(0));
        

    }

    private void Update()
    {
      
        
    }

}
