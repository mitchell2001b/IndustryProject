using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintTextObject;
    [SerializeField] string hintText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setHint()
    {      
        hintTextObject.text = hintText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
