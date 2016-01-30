using UnityEngine;
using System.Collections;

public class ButtonMaster : MonoBehaviour {

    public DoorMaster[] connectedDoors;

    public void OpenDoors()
    {
        for(int i = 0; i < connectedDoors.Length; i++)
        {
            connectedDoors[i].Add();
        }
    }

    public void CloseDoors()
    {
        for (int i = 0; i < connectedDoors.Length; i++)
        {
            connectedDoors[i].Subtract();
        }
    }
}
