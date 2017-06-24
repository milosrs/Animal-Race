using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)        //Test on collision
    {
        if (collision.gameObject.tag=="Player")
        {
            Agent a = collision.gameObject.GetComponent<Agent>();
            a.setFail(true);
        }
        if(collision.transform.gameObject)
        {
            Destroy(collision.transform.gameObject);
        }
    }
}
