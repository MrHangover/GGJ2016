using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    public static int WIDTH = 31;
    public static int HEIGHT = 18;

    enum Environment {Fire, Ice};
    Environment state = Environment.Ice;
    GameObject[] arrOfObjects;
    TileParent[,] arrOfTiles = new TileParent[WIDTH-1, HEIGHT-1];
    Rigidbody2D body;

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        arrOfObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8)
            {
                if(obj.tag == "Grass")
                {
                    arrOfTiles[(int)obj.transform.position.x, (int)obj.transform.position.y].ice = obj;
                    obj.SetActive(false);
                }
                else
                {
                    arrOfTiles[(int)obj.transform.position.x, (int)obj.transform.position.y].fire = obj;
                }
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
        foreach(TileParent parent in arrOfTiles)
        {
            if (parent.ice.active)
            {
                parent.ice.SetActive(false);
            }
            else
            {
                parent.ice.SetActive(true);
            }
            if (parent.fire.active)
            {
                parent.fire.SetActive(false);
            }
            else
            {
                parent.fire.SetActive(true);
            }
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
        //switch (state)
        //{
        //    case Environment.Fire:
        //        for (int i = 0; i < listOfObjects.Count; i++)
        //        {
        //            if (Vector2.Distance(listOfObjects[i].transform.position, transform.position) < 5f)
        //            {
        //                if (listOfObjects[i].tag == "Lava")
        //                {
        //                    listOfObjects[i].SetActive(true);
        //                }
        //                else
        //                {
        //                    listOfObjects[i].SetActive(false);
        //                }
        //            }
        //            else
        //            {
        //                if (listOfObjects[i].tag == "Lava")
        //                {
        //                    listOfObjects[i].SetActive(false);
        //                }
        //                else
        //                {
        //                    listOfObjects[i].SetActive(true);
        //                }
        //            }
        //        }
        //        break;
        //    case Environment.Ice:
        //        for (int i = 0; i < listOfObjects.Count; i++)
        //        {
        //            if (Vector2.Distance(listOfObjects[i].transform.position, transform.position) < 5f)
        //            {
        //                if (listOfObjects[i].tag == "Lava")
        //                {
        //                    listOfObjects[i].SetActive(false);
        //                }
        //                else
        //                {
        //                    listOfObjects[i].SetActive(true);
        //                }
        //            }
        //            else
        //            {
        //                if (listOfObjects[i].tag == "Lava")
        //                {
        //                    listOfObjects[i].SetActive(true);
        //                }
        //                else
        //                {
        //                    listOfObjects[i].SetActive(false);
        //                }
        //            }
        //        }
        //        break;
        //    default:
        //        break;
        //}
        int xMin = Mathf.Clamp((int)transform.position.x - 5, 0, WIDTH - 1);
        int xMax = Mathf.Clamp((int)transform.position.x + 7, 0, WIDTH - 1);
        int yMin = Mathf.Clamp((int)transform.position.y - 5, 0, HEIGHT - 1);
        int yMax = Mathf.Clamp((int)transform.position.y + 7, 0, HEIGHT - 1);

        for(int x = xMin; x < xMax; x++)
        {
            for(int y = yMin; y < yMax; y++)
            {
                if((x == xMin || x == xMax - 1) || (y == yMin || y == yMax - 1))
                {
                    if (state == Environment.Fire)
                    {
                        arrOfTiles[x, y].fire.SetActive(false);
                        arrOfTiles[x, y].ice.SetActive(true);
                    }
                    else
                    {
                        arrOfTiles[x, y].fire.SetActive(true);
                        arrOfTiles[x, y].ice.SetActive(false);
                    }
                }
                else if(state == Environment.Fire)
                {
                    arrOfTiles[x, y].fire.SetActive(true);
                    arrOfTiles[x, y].ice.SetActive(false);
                }
                else
                {
                    arrOfTiles[x, y].fire.SetActive(false);
                    arrOfTiles[x, y].ice.SetActive(true);
                }
            }
        }
    }
}
