using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent {


    public Agent()
    {
        NeuralNetwork nn = new NeuralNetwork(5, 5, new int[] { 4, 4 }, 3, 3);

        List<float> inputValues = new List<float>();

        List<float> result = nn.Calculate(inputValues);
    }



}
