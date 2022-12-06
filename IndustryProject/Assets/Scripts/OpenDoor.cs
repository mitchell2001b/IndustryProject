using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public UnityEvent<int> tryOpenDoor;

    [SerializeField] private UnityEvent openDoor;

    [SerializeField] private UnityEvent openDoorFail;

    public void OpenDoorCheck(int keyAmount)
    {
        if (keyAmount >= 3)
        {
            openDoor?.Invoke();
        }
        else
        {
            openDoorFail?.Invoke();
        }
    }
}
