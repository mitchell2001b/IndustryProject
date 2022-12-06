using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public GameObject room1;
    public GameObject room2;
    
    // Start is called before the first frame update
    void Start()
    {
        room1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackgroundChanger(){

        if(room1.activeInHierarchy == true){

            room1.SetActive(false);
            room2.SetActive(true);

        }

        else if(room2.activeInHierarchy  == true){
            
            room1.SetActive(false);
            room2.SetActive(true);

        }
    }
}
