using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class textScript : MonoBehaviour {

    [SerializeField]
    private GameObject entityHolder;
    private GA ga;

    // Use this for initialization
    void Start () {
        NinjaEntity ne = entityHolder.GetComponent<NinjaEntity>();
        ga = ne.getGa();
	}
	
	// Update is called once per frame
	void Update () {
        float bestFit = ga.GetBest().fitness;
        int current_generation = ga.getGeneration();
        Text text = this.gameObject.GetComponent<Text>();
        if (entityHolder.GetComponent<NinjaEntity>() != null)
        {

        }
	}
}
