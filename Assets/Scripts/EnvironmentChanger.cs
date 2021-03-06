﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour {

    public int width = 31;
    public int height = 18;

    // must be public to allow EnemyRegular.cs to access
    public enum Environment {Fire, Ice};
    public Color iceColor;
    public Color fireColor;
    public Environment envAtPlayer;
    public bool canChange = true;

    // Player state starts as Ice (the surrounding bubble)
    GameObject[] arrOfObjects;
    EnemyParent[] arrOfEnemyParents;
    List <EnemyParent> ListEnemyParents = new List<EnemyParent>();
    TileParent[,] arrOfTiles;
    bool isSwitching = true;
    Vector2 switchPos;
    ParticleSystem particles;

    struct TileParent
    {
        public GameObject fire;
        public GameObject ice;
    };



	void Start () {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
        if(envAtPlayer == Environment.Ice)
        {
            Camera.main.backgroundColor = fireColor;
        }
        else
        {
            Camera.main.backgroundColor = iceColor;
        }
        arrOfObjects = FindObjectsOfType<GameObject>();
        ListEnemyParents.AddRange(FindObjectsOfType<EnemyParent>());

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

                if (obj.tag == "Ice" && envAtPlayer == Environment.Ice)
                {
                    arrOfTiles[xPos, yPos].ice = obj;
                    obj.SetActive(false);
                }
                else if (obj.tag == "Ice" && envAtPlayer == Environment.Fire)
                {
                    arrOfTiles[xPos, yPos].ice = obj;
                }
                else if (obj.tag == "Fire" && envAtPlayer == Environment.Ice)
                {
                    arrOfTiles[xPos, yPos].fire = obj;
                }
                else if (obj.tag == "Fire" && envAtPlayer == Environment.Fire)
                {
                    arrOfTiles[xPos, yPos].fire = obj;
                    obj.SetActive(false);
                }
            }
            else if (obj.layer == 9)
            {
                if (obj.tag == "Ice" && envAtPlayer == Environment.Ice)
                {
                    obj.SetActive(false);
                }
                else if (obj.tag == "Fire" && envAtPlayer == Environment.Fire) {
                    obj.SetActive(false);
                }
                //else { /* Fire object*/ }
            }
        }

        int enemycount=0;

        foreach( EnemyParent enemyp in ListEnemyParents)
        {
            enemycount++;

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
        particles.Play();
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
        if (envAtPlayer == Environment.Ice)
        {
            Camera.main.backgroundColor = iceColor;
            envAtPlayer = Environment.Fire;
        }
        else
        {
            Camera.main.backgroundColor = fireColor;
            envAtPlayer = Environment.Ice;
        }
        isSwitching = true;

        foreach (TileParent parent in arrOfTiles)
        {
            if (parent.ice != null)
            {
                if (envAtPlayer == Environment.Fire)
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
        SwitchTilesNear();
    }

    
    public void AddEnemy(EnemyParent toAddEnemy)
    {
        ListEnemyParents.Add(toAddEnemy);
    }

    public void RemoveEnemy(EnemyParent toRemoveEnemy)
    {
        ListEnemyParents.Remove(toRemoveEnemy);
    }//*/

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
        foreach(EnemyParent enemyp in ListEnemyParents)
        {
            enemyp.switchState();
        }

    }

    void SwitchEnemysNear()
    {
        foreach(EnemyParent enemyp in ListEnemyParents)
        {
            GameObject enemyObj = enemyp.gameObject;
            float distance = Vector2.Distance(enemyObj.transform.position, gameObject.transform.position);
            if (distance < 5)
            {
                // if enemy state doesn't equal player state
                if(enemyp.getState() != envAtPlayer)
                {
                    enemyp.switchState();
                }
            }
            else if (distance <7 )
            {
                if(enemyp.getState() == envAtPlayer)
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
                    if (envAtPlayer == Environment.Fire)
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
                else if (envAtPlayer == Environment.Fire)
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
        if (Input.GetButtonDown("Fire1") && canChange)
        {
            SwitchState();
        }
        if(isSwitching && canChange)
            SwitchTilesNear();
        SwitchEnemysNear();
    }
}
