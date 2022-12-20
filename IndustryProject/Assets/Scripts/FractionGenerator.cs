using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System;
using UnityEngine.UI;

public class FractionGenerator : MonoBehaviour
{
    private List<float> topValues = new List<float>();
    private List<float> botValues = new List<float>();

    [SerializeField] private int botMin = 1;
    [SerializeField] private int botMax = 8;

    [SerializeField] private int maxAnswerValue = 20;

    private float answer;

    [SerializeField] private TextMeshProUGUI answerTxt;
    [SerializeField] private Image fractionPie;

    [SerializeField] private List<GameObject> topButtons;
    [SerializeField] private List<GameObject> botButtons;

    [SerializeField] private List<TextMeshProUGUI> topButtonTexts;
    [SerializeField] private List<TextMeshProUGUI> botButtonTexts;

    public float Generate()
    {
        botValues.Clear();
        topValues.Clear();
        answer = 0;
        
        while (botValues.Count < 3)
        {
            int rnd = Random.Range(botMin, botMax + 1);
            if (botValues.Contains(rnd) == false)
            {
                botValues.Add(rnd);
            }
        }

        int botAnswerIndex = Random.Range(0, 3);

        List<int> divisors = new List<int>();
        for (int i = 1; i <= botValues[botAnswerIndex]; i++)
        {
            if (botValues[botAnswerIndex] % i == 0)
            {
                divisors.Add(i);
            }
        }
        List<int> multipliers = new List<int>();
        for (int i = 1; botValues[botAnswerIndex] * i <= maxAnswerValue; i++)
        {
            if (botValues[botAnswerIndex] * i < maxAnswerValue)
            {
                multipliers.Add(i);
            }
        }
        List<int> allMultipliers = new List<int>();
        foreach (int multiplier in multipliers)
        {
            allMultipliers.Add(multiplier);
        }
        foreach (int multiplier in divisors)
        {
            allMultipliers.Add(multiplier * -1);
        }

        while (topValues.Count < 3)
        {
            int rnd = Random.Range(1, allMultipliers.Count + 1);
            rnd--;
            float tempAnswer;
            if (allMultipliers[rnd] > 0)
            {
                tempAnswer = botValues[botAnswerIndex] * allMultipliers[rnd];
            }
            else
            {
                tempAnswer = botValues[botAnswerIndex] / allMultipliers[rnd];
                tempAnswer *= -1;
            }
            if (topValues.Contains(tempAnswer) == false)
            {
                topValues.Add(tempAnswer);
            }
        }

        int topAnswerIndex = Random.Range(1, 4);
        topAnswerIndex--;
        if (topValues[topAnswerIndex] > 0)
        {
            answer = (float)(topValues[topAnswerIndex] / botValues[botAnswerIndex]);
        }

        answer = (float)Math.Round(answer, 2);

        answerTxt.text = answer.ToString();

        float piePercent = answer % 1;
        if (piePercent == 0)
        {
            piePercent = 1;
        }
        fractionPie.fillAmount = piePercent;

        Vector3 fractionPieLocation = fractionPie.rectTransform.localPosition;
        switch (answer.ToString().Length)
        {
            case 1:
                fractionPieLocation.x = 211;
                break;
            case 2:
                fractionPieLocation.x = 243;
                break;
            case 3:
                fractionPieLocation.x = 259;
                break;
            case 4:
                fractionPieLocation.x = 303;
                break;
            default:
                fractionPieLocation.x = 345;
                break;
        }

        fractionPie.rectTransform.localPosition = fractionPieLocation;

        AssignButtons();

        Debug.Log("top row answer index " + topAnswerIndex);
        Debug.Log("bottom row answer index " + botAnswerIndex);

        return answer;
    }

    private void AssignButtons()
    {
        for (int i = 0; i <= 2; i++)
        {
            topButtons[i].GetComponent<ButtonValue>().Init((int)topValues[i], 0);
            topButtonTexts[i].text = topValues[i].ToString();
            botButtons[i].GetComponent<ButtonValue>().Init((int)botValues[i], 1);
            botButtonTexts[i].text = botValues[i].ToString();
        }
    }
}
