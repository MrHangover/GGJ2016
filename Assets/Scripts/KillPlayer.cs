using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public float moveDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 temp = 
            Vector2.MoveTowards(
                gameObject.transform.position,
                GameObject.FindGameObjectWithTag("Player").transform.position,
                moveDistance);

        // keep z at -4 so player killer is visible
        gameObject.transform.position = new Vector3(temp.x, temp.y, -4);

    }
}
