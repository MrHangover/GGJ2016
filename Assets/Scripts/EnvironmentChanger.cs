﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    enum Environment {Fire, Ice};
    Environment state = Environment.Ice;
    GameObject[] arrOfObjects;
    List<GameObject> listOfObjects = new List<GameObject>();
    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        arrOfObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8)
            {
                listOfObjects.Add(obj);
            }
        }
    }
	
    public void SwitchState()
    {
        if(state == Environment.Ice)
        {
            state = Environment.Fire;
        }
        else
        {
            state = Environment.Ice;
        }
    }

	// Update is called once per frame
	void Update () {
        //Input and moving
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.velocity = input * 3f;

        if (Input.GetButtonDown("Fire1"))
        {
            SwitchState();
        }

        //Searching through tiles
        switch (state)
        {
            case Environment.Fire:
                for (int i = 0; i < listOfObjects.Count; i++)
                {
                    if (Vector2.Distance(listOfObjects[i].transform.position, transform.position) < 5f)
                    {
                        if (listOfObjects[i].tag == "Lava")
                        {
                            listOfObjects[i].SetActive(true);
                        }
                        else
                        {
                            listOfObjects[i].SetActive(false);
                        }
                    }
                    else
                    {
                        if (listOfObjects[i].tag == "Lava")
                        {
                            listOfObjects[i].SetActive(false);
                        }
                        else
                        {
                            listOfObjects[i].SetActive(true);
                        }
                    }
                }
                break;
            case Environment.Ice:
                for (int i = 0; i < listOfObjects.Count; i++)
                {
                    if (Vector2.Distance(listOfObjects[i].transform.position, transform.position) < 5f)
                    {
                        if (listOfObjects[i].tag == "Lava")
                        {
                            listOfObjects[i].SetActive(false);
                        }
                        else
                        {
                            listOfObjects[i].SetActive(true);
                        }
                    }
                    else
                    {
                        if (listOfObjects[i].tag == "Lava")
                        {
                            listOfObjects[i].SetActive(true);
                        }
                        else
                        {
                            listOfObjects[i].SetActive(false);
                        }
                    }
                }
                break;
            default:
                break;
        }
	}
}
