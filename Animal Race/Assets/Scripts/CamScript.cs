using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    private Rigidbody2D body;
    private float yMin = -15f ,yMax = 15f;
    
	// Use this for initialization
	void Start () {
        body = player.GetComponent<Rigidbody2D>();
        transform.position = new Vector3(body.position.x + 7, 0, -10);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(body.position.x + 7, 5, -10);
    }
}
