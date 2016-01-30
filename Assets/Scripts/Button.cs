using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Sprite[] sprites;
    public bool activateOnce = true;

    bool activated = false;
    ButtonMaster parentButton;
    SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        parentButton = transform.parent.GetComponent<ButtonMaster>();
	}

    void OnTriggerEnter2D()
    {
        if (!activated)
        {
            rend.sprite = sprites[1];
            parentButton.OpenDoors();
            activated = true;
        }
    }

    void OnTriggerExit2D()
    {
        if (!activateOnce && activated)
        {
            rend.sprite = sprites[0];
            parentButton.CloseDoors();
            activated = false;
        }
    }

    void OnDisable()
    {
        if (!activateOnce && activated)
        {
            rend.sprite = sprites[0];
            parentButton.CloseDoors();
            activated = false;
        }
    }
}
