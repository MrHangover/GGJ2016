using UnityEngine;
using System.Collections;

public class EnemyParent : MonoBehaviour {
    public EnvironmentChanger.Environment EnemyState;
    public GameObject fire;
    public GameObject ice;


    // Use this for initialization
    void Start () {
        fire = gameObject.GetComponentInChildren<FireEnemy>().gameObject;
        ice = gameObject.GetComponentInChildren<IceEnemy>().gameObject;

        // should probably be set by environmentChanger
        EnemyState = EnvironmentChanger.Environment.Fire;
	}

    public EnvironmentChanger.Environment getState()
    {
        return EnemyState;
    }

    public void switchState()
    {
        if(EnemyState== EnvironmentChanger.Environment.Ice)
        {
            EnemyState = EnvironmentChanger.Environment.Fire;
            ice.SetActive(false);
            fire.SetActive(true);
        }
        else
        {
            //assume EnemyState is fire and switch to Ice
            EnemyState = EnvironmentChanger.Environment.Ice;
            fire.SetActive(false);
            ice.SetActive(true);
        }
    }

	/*
	// Update is called once per frame
	void Update () {
	    
	}//*/


}
