using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] Canvas preGameCanvas;
    [SerializeField] Canvas inGameCanvas;
    [SerializeField] GameObject preGameImages;
    [SerializeField] GameObject inGameImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        preGameCanvas.enabled = false;
        //inGameCanvas.enabled = true;
        preGameImages.SetActive(false);
        inGameImages.SetActive(true);
    }
}
