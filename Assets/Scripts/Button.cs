using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Sprite[] sprites;
    ButtonMaster parentButton;

    SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        parentButton = transform.parent.GetComponent<ButtonMaster>();
	}

    void OnTriggerEnter2D()
    {
        rend.sprite = sprites[1];
        parentButton.OpenDoors();
    }

    void OnTriggerExit2D()
    {
        //rend.sprite = sprites[0];
    }
}
