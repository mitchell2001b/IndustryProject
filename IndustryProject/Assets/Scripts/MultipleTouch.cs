using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleTouch : MonoBehaviour
{
   
    private List<TouchLocation> touches = new List<TouchLocation>();
    [SerializeField] int touchec;
    [SerializeField] int listcount;
    // Update is called once per frame
    void Update()
    {
        int i = 0;
        touchec = Input.touchCount;
        listcount = touches.Count;
        while (i < Input.touchCount)
        {
            if (Input.touchCount <= 0)
            {
                break;
            }
           
            Touch touch = Input.GetTouch(i);

           
           
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 pos = touch.position;              
                if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    pointerEventData.position = touch.position;
                    List<RaycastResult> raycastResults = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerEventData, raycastResults);
                    foreach (RaycastResult r in raycastResults)
                    {
                        if(r.gameObject.tag == "draggable" && r.gameObject.GetComponent<DraggableSprite>() != null)
                        {
                            if(!r.gameObject.GetComponent<DraggableSprite>().dragLocked)
                            {
                                Debug.Log("add");
                                touches.Add(new TouchLocation(touch.fingerId, r.gameObject));
                                r.gameObject.GetComponent<DraggableSprite>().OnBeginDrag(pointerEventData);
                                break;
                            }
                           
                        }
                    }

                }                            
                
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {             
               if(touches.Find(x => x.touchId == touch.fingerId) != null)
               {
                    Debug.Log(touches.Find(x => x.touchId == touch.fingerId).objectToDrag.name);
                    touches.Find(x => x.touchId == touch.fingerId).objectToDrag.GetComponent<DraggableSprite>().OnEndDrag(null);
                    touches.Remove(touches.Find(x => x.touchId == touch.fingerId));                 
               }

            }
            else if(touch.phase == TouchPhase.Moved)
            {
                if(touches.Find(x => x.touchId == touch.fingerId) == null)
                {
                    return;
                }
                Vector3 pos = touch.position;
                //Debug.Log("pos" + pos);
                
                touches.Find(x => x.touchId == touch.fingerId).objectToDrag.transform.position = pos;
                              
            }
            
            i++;
        }
    }
    
   
}
