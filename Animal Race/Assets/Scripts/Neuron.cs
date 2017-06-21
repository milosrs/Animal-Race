using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron{

    public int inputNo;
    public List<float> weights = new List<float>();

    public float RandomizeWeights()                     //Random tezine na pocetku
    {
        float rand = Random.Range(0.0f, 2.1f);
        Debug.Log("Random number is: " + rand);
        return rand;
    }

    public float Clamp(float val, float min, float max)
    {
        if (val < min)
        {
            val = min;
        }
        if (val > max)
        {
            val = max;
        }
        return val;
    }

    public float biasWeight()
    {
        return RandomizeWeights() - RandomizeWeights();
    }

    public void initNeuron(int numInput)
    {
        inputNo = numInput;
        for(int i=0; i<numInput; i++)
        {
            weights.Add(RandomizeWeights());        //Svakom ulazu se dodaje tezina
        }

        weights.Add(biasWeight());                  //Dodaje se tezina bias ulazu
    }

    public void init(List<float> weights, int numInput)
    {
        this.weights = weights;                     //Inicijalizacija neurona
        this.inputNo = numInput;
    }
}
