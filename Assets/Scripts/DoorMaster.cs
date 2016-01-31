using UnityEngine;
using System.Collections;

public class DoorMaster : MonoBehaviour {

    public int buttonsNeededToOpen = 1;
    public bool open = false;

    int buttonsActivated = 0;
    Door[] childDoors;
	
    void Awake()
    {
        childDoors = GetComponentsInChildren<Door>();
        Debug.Log(childDoors.Length);
    }

	public void Open()
    {
        open = true;
        for(int i = 0; i < childDoors.Length; i++)
        {
            childDoors[i].Open();
        }
    }

    public void Close()
    {
        open = false;
        for (int i = 0; i < childDoors.Length; i++)
        {
            childDoors[i].Close();
        }
    }

    public void Add()
    {
        buttonsActivated++;
        if (buttonsActivated >= buttonsNeededToOpen)
            Open();
        Debug.Log(buttonsActivated);
    }

    public void Subtract()
    {
        buttonsActivated--;
        if (buttonsActivated < buttonsNeededToOpen)
            Close();
        Debug.Log(buttonsActivated);
    }
}
