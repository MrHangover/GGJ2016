using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float speed = 0.5f;

	// Use this for initialization
	void Start () {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //gameObject.GetComponent<Rigidbody2D>().velocity =
        //    Vector2.MoveTowards(gameObject.transform.position, player.transform.position, 1) * speed;
	}

    // when fireBall exits screen delete
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
