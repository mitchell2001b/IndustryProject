using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MinigameManager;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    #region Global Use

    private string sumString;
    public TextMeshProUGUI sumText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    #endregion

    #region Calculator
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
    #endregion

    #region Metric
    enum MetricSystem
    {
        meter,
        gram,
        liter
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

    public void SetMetricStartText(float sum, string convertedValue, string metricText, int score)
    {
        timerText.text = 15.ToString();
        sumText.text = sum.ToString() + " " + convertedValue + metricText;
        scoreText.text = $"{score}/3 goed";
    }

    //Generates the metric unit used in the problem. This is only for visuals and is not looked at while making a problem.
    public string GenerateMetricUnit()
    {
        int rnd = Random.Range(0, 4);
        MetricSystem metric = (MetricSystem)rnd;
        return metric switch
        {
            MetricSystem.meter => "M",
            MetricSystem.gram => "G",
            MetricSystem.liter => "L",
            _ => "M",
        };
    }
    #endregion

    #region Fraction
    public void SetFractionText(int totalQuestionCount, int score)
    {
        scoreText.text = $"{score}/{totalQuestionCount} correct";
    }

    #endregion
}
