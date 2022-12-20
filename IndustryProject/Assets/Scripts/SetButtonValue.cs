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
    public bool isPressed;

    public void SetNumberButtonValue(float number)
    {
        gameObject.name = number.ToString();
        value = number;
        GetComponentInChildren<TMP_Text>().text = value.ToString();
    }

    public void SetNumberButtonValue(float number, string metricCatagory, string metric)
    {
        gameObject.name = number.ToString();
        value = number;
        GetComponentInChildren<TMP_Text>().text = value.ToString() + " " + metricCatagory + metric;
    }

    public void SetOperatorButtonValue(string value)
    {
        gameObject.name = value;
        GetComponentInChildren<TMP_Text>().text = value;
    }

    public void CheckIfButtonIsPressed()
    {
        if (!isPressed)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    public void SetPressedFalse()
    {
        isPressed = false;
    }
}
