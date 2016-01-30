using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float speed = 0.5f;
    bool isDisappearing = false;

	// Use this for initialization
	void Start () {
	}


    // when fireBall exits screen delete
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
