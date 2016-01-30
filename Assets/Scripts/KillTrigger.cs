using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }
}
