using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {

    private int numberOfInputs;
    public List<float> weights;

    public float RandomizeWeights()                     //Random tezine na pocetku
    {
        float rand = Random.Range(-2.0f, 2.0f);
        return rand;
    }

    public Neuron(int numInput)
    {
        numberOfInputs = numInput;
        weights = new List<float>();
        
        for (int i = 0; i < numberOfInputs; i++)
        {
            weights.Add(RandomizeWeights());        //Svakom ulazu se dodaje tezina
        }
        weights.Add(RandomizeWeights());                  //Dodaje se tezina bias ulazu
    }

    public List<float> getWeights(){
        return weights;
    }

    public void setWeights(List<float> newWeights)
    {
        weights = newWeights;
    }

	public static float Sigmoid(float a)
	{ 
		return (1 / (1 + Mathf.Exp(-a)));
	}

    public float output(List<float> inputValues){
        float res = 0.0f;

        for (int i = 0; i < numberOfInputs; i++)
        {
            res += inputValues[i] * weights[i];
        }

        //jel ovo sigurno ovako
        res += weights[weights.Count]; // dodamo bias

        return Sigmoid(res);
    }

    public int getNumberOfInputs()
    {
        return numberOfInputs;
    }
    
}
