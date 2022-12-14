using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuzzleFractionScene : MonoBehaviour
{
    public string passWord;
    [SerializeField] GameObject computerOnButton;
    [SerializeField] GameObject computerOffButton;
    [SerializeField] List<GameObject> toggableObjects;
    [SerializeField] GameObject plugButton;
    [SerializeField] List<GameObject> toggableObjectsPasswordScreen;
    private bool computerIsActive;
    [SerializeField] GameObject outletOff;
    [SerializeField] GameObject outletOn;
    [SerializeField] GameObject PrinterButton;

    private bool fractionPuzzleComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        passWord = "Y94Z";
    }

    public void TurnAllToggableObjectsOff()
    {
        foreach(GameObject obj in toggableObjects)
        {
            obj.SetActive(false);
        }
        computerOnButton.SetActive(false);
        computerOffButton.SetActive(false);
        plugButton.SetActive(false);
        outletOff.SetActive(false);
        outletOn.SetActive(false);
        PrinterButton.SetActive(false);
    }

    public void TurnAllToggableObjectsOn()
    {
        foreach (GameObject obj in toggableObjects)
        {           
            obj.SetActive(true);
        }
        if(fractionPuzzleComplete)
        {
            PrinterButton.SetActive(true);
        }
        ToggleComputerButton();
    }

    public void TurnOffPasswordScreen()
    {
        foreach (GameObject obj in toggableObjectsPasswordScreen)
        {
            obj.SetActive(false);
        }    
    }

    public void TurnOnPasswordScreen()
    {
        foreach (GameObject obj in toggableObjectsPasswordScreen)
        {
            obj.SetActive(true);
        }       
    }

    public void ToggleComputerButton()
    {
        if(computerIsActive)
        {
            computerOnButton.SetActive(true);
            computerOffButton.SetActive(false);
            plugButton.SetActive(false);
            outletOn.SetActive(true);

        }
        else
        {
            computerOffButton.SetActive(true);
            computerOnButton.SetActive(false);
            plugButton.SetActive(true);
            outletOff.SetActive(true);
        }
    }

    public void SetComputerToBeOn()
    {
        computerIsActive = true;
        ToggleComputerButton();
    }

    public void SetFractionPuzzleToBeCompleted()
    {
        fractionPuzzleComplete = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
