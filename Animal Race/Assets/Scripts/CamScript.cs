using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [SerializeField]
    private Ninja player;
    private float yMin = -15f ,yMax = 15f;

    private Transform target;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(Ninja.Instance.transform.position.x + 7, 0, -10);
        target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.y > yMin && player.transform.position.y < yMax)
        {
            transform.position = new Vector3(Ninja.Instance.transform.position.x + 7, Ninja.Instance.transform.position.y + 3, -10);
        }
    }
}
