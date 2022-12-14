using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PcKeyButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] char keyValue;
    [SerializeField] TextMeshProUGUI keyText;
    [SerializeField] PcInputPasswordHandler passwordHandler;
    void Start()
    {
        SetKeyText(keyValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        passwordHandler.InputFieldTextChange(keyValue);
    }

    private void SetKeyText(char value)
    {
        keyText.text = value.ToString();
    }
}
