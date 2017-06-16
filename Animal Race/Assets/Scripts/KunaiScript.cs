using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class KunaiScript : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D kunai;
    private Vector2 direction;

	// Use this for initialization
	void Start () {
        kunai = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        kunai.velocity = direction * speed;
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void init(Vector2 direction)
    {
        this.direction = direction;
    }
}
