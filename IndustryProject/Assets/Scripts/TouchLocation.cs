using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation
{
    public int touchId;
    public GameObject objectToDrag;

    
    public TouchLocation(int newTouchId, GameObject dragObject)
    {
        touchId = newTouchId;
        
        objectToDrag = dragObject;  
    }
}
