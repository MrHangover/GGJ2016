using UnityEngine;
using System.Collections;

public class EnemyRegular : MonoBehaviour {
    
    public EnvironmentChanger.Environment EnemyState; 
    SpriteRenderer rend;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Disable()
    {
       if(rend != null)
        {
            rend.enabled = false;
        }
    }

    public void OnEnable()
    {
        if(rend != null)
        {
            rend.enabled = true;
        }
    }
}
