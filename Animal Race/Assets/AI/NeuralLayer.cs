using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralLayer{
    private int totalNeurons;
    private int totalInputs;
    private List<Neuron> neurons;

	public NeuralLayer(int numOfInputs, int numOfNeurons){
		totalInputs = numOfInputs;
		totalNeurons = numOfNeurons;
		neurons = new List<Neuron> ();

		for (int i = 0; i < numOfNeurons; i++) {
			Neuron n = new Neuron (numOfInputs);
			neurons.Add (n);
		}
	}

    public void Evaluate(List<float> input, ref List<float> output)
    {
        int inputIndex = 0;

        for (int i = 0; i < totalNeurons; i++)
        {
            float activation = 0.0f;
            for (int j = 0; j <neurons[i].getNumberOfInputs() ; j++)
            {
                activation += input[inputIndex] * neurons[i].weights[j];
                inputIndex++;
            }
            activation -= neurons[i].weights[neurons[i].getNumberOfInputs()];
            output.Add(Neuron.Sigmoid(activation));
            inputIndex = 0;
        }
    }

    public int GetNumOfNeurons()
    {
        return totalNeurons;
    }

    public void setNeurons(int neuronNumber, int inputNumber, List<Neuron> neurons)
    {
        totalNeurons = neuronNumber;
        totalInputs = inputNumber;
        this.neurons = neurons;
    }

    public List<Neuron> getNeurons()
    {
        return neurons;
    }
}
