using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public Sprite[] sprites;

    SpriteRenderer rend;
    BoxCollider2D col;
    DoorMaster parentDoor;

    void Awake () {
        rend = GetComponent<SpriteRenderer>();
        parentDoor = transform.parent.GetComponent<DoorMaster>();
        col = GetComponent<BoxCollider2D>();
	}

    public void Open()
    {
        rend.sprite = sprites[1];
        col.isTrigger = true;
    }

    public void Close()
    {
        rend.sprite = sprites[0];
        col.isTrigger = false;
    }

    void OnEnable()
    {
        if (parentDoor.open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
