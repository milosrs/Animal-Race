using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            Debug.Log("Nindza je bezbedan");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Ninja ce crci");
    }
}
