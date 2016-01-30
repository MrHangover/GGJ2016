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

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };

	// Use this for initialization
	void Start () {
        arrOfObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8)
            {
                if(obj.tag == "Ice")
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
            if (parent.ice.activeSelf)
            {
                parent.ice.SetActive(false);
            }
            else
            {
                parent.ice.SetActive(true);
            }
            if (parent.fire.activeSelf)
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
        //Input for switching tiles
        if (Input.GetButtonDown("Fire1"))
        {
            SwitchState();
        }

        //Searching through closest tiles using a kernel
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
                        if (arrOfTiles[x, y].fire != null)
                            arrOfTiles[x, y].fire.SetActive(false);
                        if (arrOfTiles[x, y].ice != null)
                            arrOfTiles[x, y].ice.SetActive(true);
                    }
                    else
                    {
                        if (arrOfTiles[x, y].fire != null)
                            arrOfTiles[x, y].fire.SetActive(true);
                        if (arrOfTiles[x, y].ice != null)
                            arrOfTiles[x, y].ice.SetActive(false);
                    }
                }
                else if(state == Environment.Fire)
                {
                    if (arrOfTiles[x, y].fire != null)
                        arrOfTiles[x, y].fire.SetActive(true);
                    if (arrOfTiles[x, y].ice != null)
                        arrOfTiles[x, y].ice.SetActive(false);
                }
                else
                {
                    if (arrOfTiles[x, y].fire != null)
                        arrOfTiles[x, y].fire.SetActive(false);
                    if (arrOfTiles[x, y].ice != null)
                        arrOfTiles[x, y].ice.SetActive(true);
                }
            }
        }
    }
}
