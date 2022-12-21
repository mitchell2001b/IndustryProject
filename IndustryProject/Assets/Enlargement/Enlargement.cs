using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlargement : MonoBehaviour
{
    private Vector3 bigScale, mediumScale, smallScale;
    private bool isBig;
    private bool isMedium;
    
    void Start()
    {
        bigScale = new Vector3(0.59f, 0.59f, 0.59f);
        mediumScale = new Vector3(0.30f, 0.30f, 0.30f);
        smallScale = new Vector3(0.15f, 0.15f, 0.15f);
        isBig = true;
        isMedium = false;
    }

    private void OnMouseDown()
    {
        if(isBig){
            transform.localScale = mediumScale;

            isBig = !isBig;
            isMedium = true;
        }

        else if(isMedium){
            transform.localScale = smallScale;

            isMedium = !isMedium;
        }

        else{
            transform.localScale = bigScale;

            isBig = true;
        }
    }
}
