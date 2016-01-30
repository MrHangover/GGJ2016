using UnityEngine;
using System.Collections;

public class EnemyGeneric : MonoBehaviour {

	

    public float moveDistance;
    public GameObject projectile;
    public float projectileSpeed;
    public float everyNumSecFire;

    public bool canKill = false;
    public bool doesShoot = false;
    public bool doesMove = true;
    

    // ensure Coroutine of shooting doesn't shoot twice 
    // before next seconds needed
    bool isShooting = false;
    GameObject lastProj = null;

    public bool Kills()
    {
        return canKill;
    }

    // Update is called once per frame
    void Update()
    {

        if (doesMove)
            MoveTowardsPlayer();
        if (doesShoot && !isShooting)
            StartCoroutine(ShootingProjectile());
    }

    void MoveTowardsPlayer()
    {
        // Move self towards enemy
        Vector2 temp =
            Vector2.MoveTowards(
                gameObject.transform.position,
                GameObject.FindGameObjectWithTag("Player").transform.position,
                moveDistance);

        // keep z at -4 so player killer is visible
        gameObject.transform.parent.transform.position = new Vector3(temp.x, temp.y, -4);
    }

    void OnDisable()
    {
        isShooting = false;
    }

    void OnEnable()
    {
        ShootingProjectile();
    }

    IEnumerator ShootingProjectile()
    {
        isShooting = true;
        ShootProjectile();
        yield return new WaitForSeconds(3);
        isShooting = false;
    }

    void ShootProjectile()
    {
        
        //DestroyLastProjectile(lastProj);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = (player.transform.position - gameObject.transform.position).normalized * projectileSpeed; 
        //lastProj = proj;
        StartCoroutine(DelayedDestroyProjectile(proj));
    }

    void DestroyLastProjectile(GameObject lastProj)
    {
        if (lastProj != null)
            DestroyObject(lastProj);
    }

    IEnumerator DelayedDestroyProjectile(GameObject projectile)
    { 
        yield return new WaitForSeconds(10);
        DestroyObject(projectile);
    }
}
