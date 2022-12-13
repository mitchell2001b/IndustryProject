using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MinigameManager;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class UIManager : MonoBehaviour
{
    private string sumString;
    public TextMeshProUGUI sumText;
    public TextMeshProUGUI timerText;

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

    //Puts the correct metric and number on the button
    public void SetButtonValues(GameObject[] numberButtons, List<float> convertedValues, List<string> convertedValuesText, string metricText)
    {
        int counter = 0;
        foreach (var button in numberButtons)
        {
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(
                convertedValues[counter],
                convertedValuesText[counter],
                metricText);
            counter++;
        }
    }

    public IEnumerator UpdateTimer(int time, int second)
    {
        for (int i = time; i >= 0; i--)
        {
            yield return new WaitForSeconds(second);
            timerText.text = i.ToString();
        }
    }
}
