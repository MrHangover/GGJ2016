using UnityEngine;
using System.Collections;

public class FireEnemy : EnemyRegular {

    public float moveDistance;

    void Start()
    {
        EnemyState = EnvironmentChanger.Environment.Fire;
    }
	
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

    }
}
