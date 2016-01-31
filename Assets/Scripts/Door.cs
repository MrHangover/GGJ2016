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
        Debug.Log("Opening!");
    }

    public void Close()
    {
        rend.sprite = sprites[0];
        col.isTrigger = false;
        Debug.Log("Closing!");
    }

    void OnEnable()
    {
        Invoke("CheckDoor", 0.1f);
    }
    
    void CheckDoor()
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
