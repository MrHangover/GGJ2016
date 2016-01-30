using UnityEngine;
using System.Collections;

public class FireEnemyStatic : MonoBehaviour {

    GameObject player;
    Animator anim;
    public GameObject projectile;
    public float distanceToShoot = 5f;
    public float shootDelay = 4f;

    void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.name);
    }
	
    void OnEnable()
    {
        StartCoroutine("Charge");
    }

    void OnDisable()
    {
        StopCoroutine("Charge");
    }

    IEnumerator Charge()
    {
        while (true) { 
            yield return new WaitForSeconds(shootDelay);
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToShoot &&
                Mathf.Abs(player.transform.position.y - transform.position.y) < distanceToShoot)
            {
                anim.SetBool("isCharging", true);
                Invoke("Shoot", 0.32f);
            }
        }
    }

	void Shoot()
    {
        anim.SetBool("isCharging", false);
        GameObject projClone = (GameObject)Instantiate(projectile, transform.position + Vector3.back, Quaternion.identity);
    }
}
