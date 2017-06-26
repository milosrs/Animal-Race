using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin = 10f;
    public float spawnMax = 20f;
    private Rigidbody2D now;
    private float lastX;
    

    // Use this for initialization
    void Awake () {
        if (obj.Length > 1)
        {
            now = GetComponent<Rigidbody2D>();
            lastX = now.position.x;
            Spawn();
        }
        else
        {
            Spawn();
        }
	}

    private void FixedUpdate()
    {
        if (obj.Length > 1)
        {
            if (now.position.x > lastX && (now.position.x > lastX + 70))
            {
                Spawn();
            }
        }
    }

    // Update is called once per frame
    void Spawn() {
        if (obj.Length > 1)
        {
            Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
            lastX = now.position.x;
        }
        else
        {
            Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
            Invoke("Spawn", 1.2f);
        }
    }
}
