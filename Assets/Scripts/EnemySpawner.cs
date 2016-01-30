using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


    public float moveDistance;
    public GameObject enemyToSpawn;
    public float spawnDelay = 3f;

    public bool canKill = false;
    public bool doesShoot = false;
    public bool doesMove = true;

    bool isSpawning;

    // Use this for initialization
    void Start () {
        isSpawning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isSpawning)
            SpawnEnemy();
        else
            Rest();
	}

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(10);
        isSpawning = true;
    }
}
