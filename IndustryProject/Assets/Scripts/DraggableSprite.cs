using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Image image;
    public Vector3 startPosition;
    [SerializeField] GameObject dropzoneObject;
    [SerializeField] bool disableDropzoneOnDragEnd = true;
    public bool touchControlsOn;
    public bool dragLocked { get; private set; }

    public void OnBeginDrag(PointerEventData eventData)
    {    
        if(!dragLocked)
        {
            image.raycastTarget = false;
            transform.position = startPosition;
            dropzoneObject.SetActive(true);
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragLocked)
        {
            if (!touchControlsOn)
            {
                Debug.Log("drag");
                transform.position = Input.mousePosition;
                dropzoneObject.SetActive(true);
            }
        }
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dragLocked)
        {
            image.raycastTarget = true;
            transform.position = startPosition;

            if (disableDropzoneOnDragEnd)
            {
                dropzoneObject.SetActive(false);
            }
        }
        
        
        
    } 

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        startPosition = transform.position;
       
    }
   
    public void SetDragLocked()
    {
        dragLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
