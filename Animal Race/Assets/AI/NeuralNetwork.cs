﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeuralNetwork{

    private int inputAmount;
    private int outputAmount;
	private List<NeuralLayer> layers;
    private List<float> inputs, outputs;

    public NeuralNetwork(int numOfInput, int numOfInputNeurons, int[] numOfHidden, int numOfOutput, int numOfOutputNeurons)
    {
        inputAmount = numOfInput;
        outputAmount = numOfOutput;
        layers = new List<NeuralLayer>();
        outputs = new List<float>();
        inputs = new List<float>();

        NeuralLayer inputLayer = new NeuralLayer(numOfInput, numOfInputNeurons);
        layers.Add(inputLayer);

        //broj unutrasnjih slojeva je velicina liste
        //za svaki unutrasnji sloj imamo koliko zelimo neurona u njemu
        //broj inputa je broj svih izlaza iz prethodnog sloja
        for (int i = 0; i < numOfHidden.Length ; i++)
        {
            NeuralLayer hiddenLayer = new NeuralLayer(layers[layers.Count-1].GetNumOfNeurons(), numOfHidden[i]);
            layers.Add(hiddenLayer);
        }

        NeuralLayer outputLayer = new NeuralLayer(numOfOutput, numOfOutputNeurons);
        layers.Add(outputLayer);
    }

    public List<float> Calculate(List<float> inputValues)
    {
        List<float> outputValues = new List<float>();
        List<float> tempValues = inputValues;

        for (int i = 0; i < layers.Count; i++) //racunamo izmedju slojeva 
        {
            layers[i].Evaluate(tempValues,ref outputValues);
            tempValues = outputValues;
        }
        //outputValues = tempValues;
        return outputValues;
    }

    public void refreshNetwork()
    {
        outputs.Clear();

        for(int i=0; i<layers.Count-1; i++)
        {
            layers[i].Evaluate(inputs, ref outputs);        //Reevaluacija
            inputs = outputs;
        }
        inputs = outputs;
        layers[layers.Count - 1].Evaluate(inputs, ref outputs); //-||-
    }

    public void releaseNet()
    {
        layers.Clear();
        layers = new List<NeuralLayer>(); 
    }

    public void setInput(List<float> input)
    {
        this.inputs = input;
    }

    public float getOutput(int ID)
    {
        if (ID >= outputAmount)
        {
            return 0.0f;
        }
        return outputs[ID];
    }

    public int hiddenLayerCount()
    {
        return layers.Count - 2;
    }

    public void fromGenome(Genome g)
    {
        List<float> nw = new List<float>();
        for(int i=0; i<g.weights.Count; i++)
        {
            nw.Add(g.weights[i]);
        }
        
        for (int i=0; i<layers.Count; i++)
        {
            for(int j=0; j<layers[i].GetNumOfNeurons(); j++)
            {
                List<float> layerWeight = new List<float>();
                for(int k=0; k < layers[i].getNeurons()[j].weights.Count; k++)
                {
                    try
                    {
                        layerWeight.Add(nw[k]);
                        nw.RemoveAt(k);
                    }
                    catch
                    {
                        layerWeight.Add(UnityEngine.Random.Range(-0.3f, 0.3f));
                    }
                }
                layers[i].getNeurons()[j].setWeights(layerWeight);
            }
        }
    }

    public void toGenome(Genome g)
    {
        g.weights.Clear();
        for(int i=0; i<layers.Count; i++)
        {
            for(int j=0; j<layers[i].GetNumOfNeurons(); j++)
            {
                g.weights.AddRange(layers[i].getNeurons()[j].weights);
            }
        }
    }
}