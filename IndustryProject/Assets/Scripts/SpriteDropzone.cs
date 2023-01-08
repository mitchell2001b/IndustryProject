using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SpriteDropzone : MonoBehaviour, IDropHandler
{
    [SerializeField] UnityEvent onSpritedDropped;
    [SerializeField] GameObject spriteDrop;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if(spriteDrop.name == dropped.name)
        {
            dropped.transform.position = gameObject.transform.position;
            dropped.GetComponent<DraggableSprite>().startPosition = transform.position;
            dropped.GetComponent<DraggableSprite>().enabled = false;
            dropped.GetComponent<DraggableSprite>().SetDragLocked();
            onSpritedDropped.Invoke();
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
