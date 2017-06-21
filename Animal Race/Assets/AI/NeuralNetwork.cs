using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeuralNetwork{

    private int inputAmount;
    private int outputAmount;
	private List<NeuralLayer> layers;

	public NeuralNetwork(int numOfInput, int numOfOutput, int numOfHiddenLayers, int numOfHiddenLayerNeurons){
		inputAmount = numOfInput;
		outputAmount = numOfOutput;
		layers = new List<NeuralLayer> ();
	
		NeuralLayer inputLayer = new NeuralLayer (numOfInput, );
	
	
	}

	public List<float> evaluate(){
	
	
	}



}