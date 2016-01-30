using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    public static int WIDTH = 31;
    public static int HEIGHT = 18;

    public enum Environment {Fire, Ice};
    
    // Player state starts as Ice (the surrounding bubble)
    Environment state = Environment.Ice;
    GameObject[] arrOfObjects;
    EnemyRegular[] arrOfEnemyObjects;
    TileParent[,] arrOfTiles = new TileParent[WIDTH-1, HEIGHT-1];

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };



	// Use this for initialization
	void Start () {
        arrOfObjects = FindObjectsOfType<GameObject>();
        arrOfEnemyObjects = FindObjectsOfType<EnemyRegular>();
        
        /*
            Set all GameObjects in layer 8 ("env" layer) with Ice state disabled
            and fire state enabled

            layer 9 is enemy layer
        */

        foreach(GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8 )
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
            // note future consolidation of tag checking
            // check enemy layer and deactivate all ice objects
            
            else if (obj.layer == 9)
            {
                if(obj.tag=="Ice")
                {
                    obj.SetActive(false);
                    
                }
                else { /* Fire object*/ }
                
            }
        }

        
        
    }
	
    public void SwitchState()
    {

        /*
            Switch the current player state enum
        */
        if(state == Environment.Ice)
        {
            state = Environment.Fire;
        }
        else
        {
            state = Environment.Ice;
        }

        /*
            Deactive/active and switch all tile
        */

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

        /*
            Switch all enemy objects
        */
        foreach (EnemyRegular enemy in arrOfEnemyObjects)
        {
            GameObject tmpObj = enemy.gameObject;
            if (tmpObj.tag == "ice")
            {
                // currently does nothing, can be removed
            }
            else if (tmpObj.tag == "fire")
            {

            }
            else
            {
                Debug.Log("Error no tag found on enemy");
            }

            SwitchEnemy(enemy);


        }

    }

    void SwitchEnemy(EnemyRegular enemy)
    {
        GameObject tmpObj = enemy.gameObject;
        if (tmpObj.activeSelf)
        {
            tmpObj.SetActive(false);
        }
        else
        {
            tmpObj.SetActive(true);
        }
    }

    void SwitchEnemysNear()
    {
        foreach (EnemyRegular enemy in arrOfEnemyObjects)
        {
            GameObject tmpObj = enemy.gameObject;
            if(Vector2.Distance(enemy.transform.position,gameObject.transform.position)<5)
            {
                // if player state and enemy state not the same, set it as the same
                if(state != enemy.EnemyState)
                {
                    tmpObj.gameObject.SetActive(false);
                }
                else
                {
                    tmpObj.gameObject.SetActive(true);
                }
            }
        }
    }

    

    // Switch all tiles near the player
    void SwitchTilesNear()
    {
        //Searching through closest tiles using a kernel
        int xMin = Mathf.Clamp((int)transform.position.x - 5, 0, WIDTH - 1);
        int xMax = Mathf.Clamp((int)transform.position.x + 7, 0, WIDTH - 1);
        int yMin = Mathf.Clamp((int)transform.position.y - 5, 0, HEIGHT - 1);
        int yMax = Mathf.Clamp((int)transform.position.y + 7, 0, HEIGHT - 1);

        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                if ((x == xMin || x == xMax - 1) || (y == yMin || y == yMax - 1))
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
                else if (state == Environment.Fire)
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

    


    // Update is called once per frame
    void Update () {
        //Input for switching tiles
        if (Input.GetButtonDown("Fire1"))
        {
            SwitchState();
        }

        SwitchTilesNear();
        SwitchEnemysNear();


    }

    
}
