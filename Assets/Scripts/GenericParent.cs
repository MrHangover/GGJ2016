﻿using UnityEngine;
using System.Collections;

public class GenericParent : MonoBehaviour {

    public EnvironmentChanger.Environment EnemyState;
    public GameObject fire;
    public GameObject ice;


    // Use this for initialization
    void Awake()
    {
        fire = gameObject.GetComponentInChildren<FireGeneric>().gameObject;
        ice = gameObject.GetComponentInChildren<IceGeneric>().gameObject;

        // should probably be set by environmentChanger
        EnemyState = EnvironmentChanger.Environment.Fire;
    }

    void Start()
    {
        Debug.Log("enemyparent ice stop");
        ice.SetActive(false);
    }

    public EnvironmentChanger.Environment getState()
    {
        return EnemyState;
    }

    public void switchState()
    {
        Debug.Log("switch State called");
        if (EnemyState == EnvironmentChanger.Environment.Ice)
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

}