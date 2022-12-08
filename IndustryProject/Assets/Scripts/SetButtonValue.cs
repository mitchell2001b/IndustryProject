using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonValue : MonoBehaviour
{
    public string buttonTextNumber;
    public float value;

    public void SetNumberButtonValue(float number)
    {
        gameObject.name = number.ToString();
        value = number;
        GetComponentInChildren<TMP_Text>().text = value.ToString();
    }

    public void SetNumberButtonValue(float number, string metric)
    {
        gameObject.name = number.ToString();
        value = number;
        GetComponentInChildren<TMP_Text>().text = value.ToString() + " " + metric;
    }

    public void SetOperatorButtonValue(string value)
    {
        gameObject.name = value;
        GetComponentInChildren<TMP_Text>().text = value;
    }
}
