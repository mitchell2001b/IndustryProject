using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonValue : MonoBehaviour
{
    public string buttonTextNumber;
    public int value;

    public void SetNumberButtonValue(int number)
    {
        gameObject.name = number.ToString();
        value = number;
        GetComponentInChildren<TMP_Text>().text = value.ToString();
    }

    public void SetOperatorButtonValue(string value)
    {
        gameObject.name = value;
        GetComponentInChildren<TMP_Text>().text = value;
    }
}
