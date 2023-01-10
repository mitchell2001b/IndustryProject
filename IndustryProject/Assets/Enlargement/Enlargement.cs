using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlargement : MonoBehaviour
{
    private Vector3 bigScale, mediumScale, smallScale;
    private bool isBig;
    private bool isMedium;
    private bool isSmall;
    
    void Start()
    {
        bigScale = new Vector3(0.59f, 0.59f, 0.59f);
        mediumScale = new Vector3(0.30f, 0.30f, 0.30f);
        smallScale = new Vector3(0.15f, 0.15f, 0.15f);
        isMedium = false;
        isSmall = true;
        transform.localScale = smallScale;
    }

    private void OnMouseDown()
    {
        if(isSmall){
            transform.localScale = mediumScale;

            isSmall = !isSmall;
            isMedium = true;
        }

        else{
            transform.localScale = bigScale;

            isMedium = !isMedium;
            isBig = true;
        }
    }
}
