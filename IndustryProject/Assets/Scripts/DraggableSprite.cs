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
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin");
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        transform.position = Input.mousePosition;
        dropzoneObject.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.position = startPosition;
        if(disableDropzoneOnDragEnd)
        {
            dropzoneObject.SetActive(false);
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
