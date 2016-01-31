using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {


    public float moveDistance;
    public EnemyParent enemyToSpawn;
    public float spawnDelay = 3f;
    List<EnemyParent> ListEnemySpawns = new List<EnemyParent>();

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
        {
            StartCoroutine(  SpawnEnemy());
            isSpawning = true;
        }
        else
        {
            StartCoroutine( Rest());
        }
    }

    int count = 0;        

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("Spawn count: " + count);
            count++;
            EnemyParent temp = (EnemyParent) Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
            GameObject.FindGameObjectWithTag("Player").GetComponent<EnvironmentChanger>().AddEnemy(temp);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator Rest()
    {
        Debug.Log("resting");
        yield return new WaitForSeconds(10);
        isSpawning = true;
    }
}
