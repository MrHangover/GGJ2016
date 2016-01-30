using UnityEngine;
using System.Collections;

public class FireEnemy : EnemyRegular {

    public float moveDistance;
    public GameObject projectile;
    public float speed;
    public float everyNumSecFire;
    bool isRunning = false;


    // Update is called once per frame
    void Update () {
        
        // overwrite Hexagon Update


        Vector2 temp = 
            Vector2.MoveTowards(
                gameObject.transform.position,
                GameObject.FindGameObjectWithTag("Player").transform.position,
                moveDistance);

        // keep z at -4 so player killer is visible
        gameObject.transform.parent.transform.position = new Vector3(temp.x, temp.y, -4);

        if(!isRunning)
            StartCoroutine(ShootingProjectile());
    }


    void OnDisable()
    {
        isRunning = false;
    }

    void OnEnable()
    {
        ShootingProjectile();
    }

    IEnumerator ShootingProjectile()
    {
        isRunning = true;
        yield return new WaitForSeconds(3);
        ShootProjectile();
        isRunning = false;
    }

    void ShootProjectile()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        Debug.Log(player.transform.position);

        proj.GetComponent<Rigidbody2D>().velocity = (player.transform.position - gameObject.transform.position).normalized * speed;
    }
}
