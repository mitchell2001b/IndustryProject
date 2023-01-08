using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableObjectsHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> touchAbleObjects = new List<GameObject>();  
    [SerializeField] bool touchControlsOn = true;
    // Start is called before the first frame update
    void Start()
    {
          foreach (GameObject obj in touchAbleObjects)
          {
             if (touchControlsOn)
             {
                obj.GetComponent<DraggableSprite>().touchControlsOn = true;
             }
             else
             {
                obj.GetComponent<DraggableSprite>().touchControlsOn = false;
                Camera.main.gameObject.GetComponent<MultipleTouch>().enabled = false;
             }
                
          }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
