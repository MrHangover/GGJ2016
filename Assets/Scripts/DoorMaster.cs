using UnityEngine;
using System.Collections;

public class DoorMaster : MonoBehaviour {

    public bool open = false;
    Door[] childDoors;
	
    void Start()
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
}
