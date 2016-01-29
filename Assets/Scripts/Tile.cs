using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    BoxCollider2D col;
    SpriteRenderer rend;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    public void Disable()
    {
        if(col != null)
        {
            col.enabled = false;
        }
        if(rend != null)
        {
            rend.enabled = false;
        }
    }

    public void OnEnable()
    {
        if (col != null)
        {
            col.enabled = true;
        }
        if (rend != null)
        {
            rend.enabled = true;
        }
    }
}
