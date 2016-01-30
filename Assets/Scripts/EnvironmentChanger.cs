using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    public int width = 31;
    public int height = 18;

    // must be public to allow EnemyRegular.cs to access
    public enum Environment {Fire, Ice};
    
    // Player state starts as Ice (the surrounding bubble)
    Environment state = Environment.Ice;
    GameObject[] arrOfObjects;
    EnemyParent[] arrOfEnemyParents;
    TileParent[,] arrOfTiles = new TileParent[WIDTH-1, HEIGHT-1];
    EnemyRegular[] arrOfEnemyObjects;
    TileParent[,] arrOfTiles;
    bool isSwitching = true;
    Vector2 switchPos;

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };



    

	// Use this for initialization
	void Start () {
        arrOfObjects = FindObjectsOfType<GameObject>();
        arrOfEnemyParents = FindObjectsOfType<EnemyParent>();
        arrOfEnemyObjects = FindObjectsOfType<EnemyRegular>();
        /*
            Set all GameObjects in layer 8 ("env" layer) with Ice state disabled
            and fire state enabled

            layer 9 is enemy layer
        */
        int minX, maxX, minY, maxY;
        GameObject refTile;
        refTile = GameObject.FindGameObjectWithTag("Ice");
        minX = (int)refTile.transform.position.x;
        maxX = (int)refTile.transform.position.x;
        minY = (int)refTile.transform.position.y;
        maxY = (int)refTile.transform.position.y;


        foreach (GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8)
            {
                int xPos = (int)obj.transform.position.x;
                int yPos = (int)obj.transform.position.y;

                if (xPos < minX)
                    minX = xPos;
                else if (xPos > maxX)
                    maxX = xPos;
                if (yPos < minY)
                    minY = yPos;
                else if (yPos > maxY)
                    maxY = yPos;
            }
        }

        width = maxX - minX + 2;
        height = maxY - minY + 2;
        arrOfTiles = new TileParent[maxX - minX + 1, maxY - minY + 1];
        Debug.Log("X: " + width.ToString() + "\tY: " + height.ToString());

        foreach (GameObject obj in arrOfObjects)
        {
            if (obj.layer == 8)
            {
                int xPos = (int)obj.transform.position.x;
                int yPos = (int)obj.transform.position.y;

                if (obj.tag == "Ice")
                {
                    arrOfTiles[xPos, yPos].ice = obj;
                    obj.SetActive(false);
                }
                else
                {
                    arrOfTiles[xPos, yPos].fire = obj;
                }
            }
            // note future consolidation of tag checking
            // check enemy layer and deactivate all ice objects

            else if (obj.layer == 9)
            {
                if (obj.tag == "Ice")
                {
                    obj.SetActive(false);

                }
                else { /* Fire object*/ }
            }

            

        }

        foreach( EnemyParent enemyp in arrOfEnemyParents)
        {
            // if enemy parent is Ice 
            // switch it so it starts as fire
            if(enemyp.getState() == Environment.Ice)
            {
                enemyp.switchState();
            }
        }
    }

    IEnumerator SwitchTiles()
    {
        bool done = false;
        int i = 0;
        while(done == false)
        {
            int xMin = Mathf.Clamp((int)switchPos.x - i, 0, width - 1);
            int xMax = Mathf.Clamp((int)switchPos.x + i + 2, 0, width - 1);
            int yMin = Mathf.Clamp((int)switchPos.y - i, 0, height - 1);
            int yMax = Mathf.Clamp((int)switchPos.y + i + 2, 0, height - 1);

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if ((x == xMin || x == xMax - 1) || (y == yMin || y == yMax - 1))
                    {
                        if (arrOfTiles[x, y].fire != null && arrOfTiles[x, y].ice != null)
                        {
                            if (arrOfTiles[x, y].ice.activeSelf)
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
            if(xMin == 0 || xMax == width - 1 || yMin == 0 || yMax == height - 1)
            {
                done = true;
            }
            i++;
            yield return new WaitForSeconds(0.02f);
        }
        if (state == Environment.Ice)
        {
            state = Environment.Fire;
        }
        else
        {
            state = Environment.Ice;
        }
        isSwitching = true;

        foreach (TileParent parent in arrOfTiles)
        {
            if (parent.ice != null)
            {
                if (state == Environment.Fire)
                {
                    parent.ice.SetActive(true);
                    parent.fire.SetActive(false);
                }
                else
                {
                    parent.ice.SetActive(false);
                    parent.fire.SetActive(true);
                }
            }
        }
    }

    public void SwitchState()
    {
        /*
            Switch the current player state enum
        */


        /*
            Deactive/active and switch all tile
        */

        switchPos = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine("SwitchTiles");
        isSwitching = false;

        /*
            Switch all enemy objects
        */
        foreach(EnemyParent enemyp in arrOfEnemyParents)
        {
            enemyp.switchState();
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
        int xMin = Mathf.Clamp((int)transform.position.x - 5, 0, width - 1);
        int xMax = Mathf.Clamp((int)transform.position.x + 7, 0, width - 1);
        int yMin = Mathf.Clamp((int)transform.position.y - 5, 0, height - 1);
        int yMax = Mathf.Clamp((int)transform.position.y + 7, 0, height - 1);

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
        if(isSwitching)
            SwitchTilesNear();
        SwitchEnemysNear();
    }
}
