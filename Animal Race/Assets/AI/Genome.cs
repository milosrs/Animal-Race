using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Genome {

    public float fitness;           //mera prilagodjenosti
    public List<float> weights;     //unutrasnja reprezentacija
}
