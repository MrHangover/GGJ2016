using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FireStaticShot : MonoBehaviour {

    public float shotSpeed = 10f;

    GameObject player;
    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(player.transform.position.x - transform.position.x,
                      player.transform.position.y - transform.position.y).normalized * shotSpeed);
        Destroy(gameObject, 5f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
