using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PcInputPasswordHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pcInputField;
    private MiniPuzzleFractionScene puzzleHandler;
    // Start is called before the first frame update
    void Start()
    {
        puzzleHandler = GetComponent<MiniPuzzleFractionScene>();
    }

    public void InputFieldTextChange(char newChar)
    {
        if (pcInputField.text.Length == 4) return;
        if(pcInputField.text.Length == 0)
        {
            pcInputField.text = newChar.ToString();
        }
        else
        {
            pcInputField.text = pcInputField.text + newChar.ToString();
        }

    }

    public void ClearInputField()
    {
        pcInputField.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if(puzzleHandler.passWord == pcInputField.text)
        {
            puzzleHandler.TurnOffPasswordScreen();
        }
    }
}
