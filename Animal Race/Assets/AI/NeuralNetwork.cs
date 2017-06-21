using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeuralNetwork{

    private int inputAmount;
    private int outputAmount;
	private List<NeuralLayer> layers;

    public NeuralNetwork(int numOfInput, int numOfInputNeurons, int[] numOfHidden, int numOfOutput, int numOfOutputNeurons)
    {
        inputAmount = numOfInput;
        outputAmount = numOfOutput;
        layers = new List<NeuralLayer>();

        NeuralLayer inputLayer = new NeuralLayer(numOfInput, numOfInputNeurons);
        layers.Add(inputLayer);

        //broj unutrasnjih slojeva je velicina liste
        //za svaki unutrasnji sloj imamo koliko zelimo neurona u njemu
        //broj inputa je broj svih izlaza iz prethodnog sloja
        for (int i = 0; i < numOfHidden.Length ; i++)
        {
            NeuralLayer hiddenLayer = new NeuralLayer(layers[layers.Capacity-1].GetNumOfNeurons(), numOfHidden[i]);
            layers.Add(hiddenLayer);
        }

        NeuralLayer outputLayer = new NeuralLayer(numOfOutput, numOfOutputNeurons);
        layers.Add(outputLayer);
    }

    public List<float> Calculate(List<float> inputValues)
    {
        List<float> outputValues = new List<float>();
        List<float> tempValues = inputValues;

        for (int i = 0; i < layers.Capacity; i++) //racunamo izmedju slojeva 
        {
            layers[i].Evaluate(tempValues,ref outputValues);
            tempValues = outputValues;
        }
        //outputValues = tempValues;
        return outputValues;
    }
}