using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleTouch : MonoBehaviour
{
   
    private List<TouchLocation> touches = new List<TouchLocation>();
    [SerializeField] int touchCount;
    [SerializeField] int touchesInListCount;
    [SerializeField] List<int> ids = new List<int>();
    
    // Update is called once per frame
    void Update()
    {
        int i = 0;
        touchCount = Input.touchCount;
        touchesInListCount = touches.Count;
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
                    pointerEventData.position = pos;
                    List<RaycastResult> raycastResults = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerEventData, raycastResults);
                    foreach (RaycastResult r in raycastResults)
                    {
                        if(r.gameObject.tag == "draggable" && r.gameObject.GetComponent<DraggableSprite>() != null)
                        {
                            if(!r.gameObject.GetComponent<DraggableSprite>().dragLocked)
                            {
                                Debug.Log("add");
                                ids.Add(touch.fingerId);
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
                    ids.Remove(ids.Find(x => x == touch.fingerId));
               }

            }
            else if(touch.phase == TouchPhase.Moved)
            {
                if(touches.Find(x => x.touchId == touch.fingerId) != null)
                {
                    touches.Find(x => x.touchId == touch.fingerId).objectToDrag.transform.position = touch.position;
                }
                //Vector3 pos = touch.position;
                //Debug.Log("pos" + pos);
                
                
                              
            }
            
            i++;
        }
    }
    
   
}
