using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    public static int WIDTH = 31;
    public static int HEIGHT = 18;

    // must be public to allow EnemyRegular.cs to access
    public enum Environment {Fire, Ice};
    
    // Player state starts as Ice (the surrounding bubble)
    Environment state = Environment.Ice;
    GameObject[] arrOfObjects;


    EnemyParent[] arrOfEnemyParents;
    
    TileParent[,] arrOfTiles = new TileParent[WIDTH-1, HEIGHT-1];

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };



    

	// Use this for initialization
	void Start () {
        arrOfObjects = FindObjectsOfType<GameObject>();

        arrOfEnemyParents = FindObjectsOfType<EnemyParent>();
        


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
                
            }

            

        }

        int enemycount=0;

        foreach( EnemyParent enemyp in arrOfEnemyParents)
        {
            enemycount++;
            Debug.Log(enemyp.getState());

            // if enemy parent is Ice 
            // switch it so it starts as fire
            if(enemyp.getState() == Environment.Ice)
            {
                Debug.Log("switch state");
                enemyp.switchState();
            }
        }
        Debug.Log(enemycount);



        
        
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
        foreach(EnemyParent enemyp in arrOfEnemyParents)
        {
            enemyp.switchState();
        }

    }

    void SwitchEnemy(GameObject tempObj)
    {
        if (tempObj.activeSelf)
        {
            tempObj.SetActive(false);
        }
        else
        {
            tempObj.SetActive(true);
        }
    }

    void SwitchEnemysNear()
    {
        foreach(EnemyParent enemyp in arrOfEnemyParents)
        {
            GameObject enemyObj = enemyp.gameObject;
            float distance = Vector2.Distance(enemyObj.transform.position, gameObject.transform.position);
            if (distance < 5)
            {
                // if enemy state doesn't equal player state
                if(enemyp.getState() != state)
                {
                    enemyp.switchState();
                }
            }
            else if (distance <7 )
            {
                if(enemyp.getState() == state)
                {
                    enemyp.switchState();
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
                else if (state == Environment.Fire)
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
