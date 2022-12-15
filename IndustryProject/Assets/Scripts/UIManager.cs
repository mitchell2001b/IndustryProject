using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class UIManager : MonoBehaviour
{
    private string sumString;
    public TextMeshProUGUI sumText;

    public string SetOperator(int counter)
    {
        if (counter == 0)
            return "+";
        else if (counter == 1)
            return "-";
        else if (counter == 2)
            return "*";
        else if (counter == 3)
            return "/";
        return "";
    }

    public void UpdateAnswerText(float questionAnswer, float a, float b, string mathOperator)
    {
        questionAnswer = (float)Math.Round(questionAnswer, 2);
        sumString = a + " " + mathOperator + " " + b + " = " + questionAnswer;
        sumText.text = sumString;
    }
}
