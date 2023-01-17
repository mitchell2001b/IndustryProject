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
    [SerializeField] private int botMax = 10;

    [SerializeField] private int topMin = 1;
    [SerializeField] private int topMax = 10;

    [SerializeField] private int maxAnswerValue = 3;

    private float answer;

    [SerializeField] private TextMeshProUGUI answerTxt;
    [SerializeField] private Transform fractionPies;

    [SerializeField] private List<GameObject> topButtons;
    [SerializeField] private List<GameObject> botButtons;

    [SerializeField] private List<Image> pies;

    [SerializeField] private List<TextMeshProUGUI> topButtonTexts;
    [SerializeField] private List<TextMeshProUGUI> botButtonTexts;

    

    public float Generate()
    {
        botValues.Clear();
        topValues.Clear();
        answer = 0;

        while (topValues.Count < 3)
        {
            int rnd = Random.Range(topMin, topMax + 1);
            if (topValues.Contains(rnd) == false)
            {
                topValues.Add(rnd);
            }
        }
      
        int highestTopValue = 0;

        foreach (int value in topValues)
        {           
            if (value > highestTopValue)
            {
                highestTopValue = value;
            }
        }

        while (botValues.Count < 3)
        {
            int rnd = Random.Range(botMin, botMax + 1);
            if (botValues.Contains(rnd) == false && (float)highestTopValue / rnd <= maxAnswerValue)
            {
                botValues.Add(rnd);
            }
        }

        int topAnswerIndex = Random.Range(0, topValues.Count);
        int botAnswerIndex = Random.Range(0, botValues.Count);

        answer = (float)(topValues[topAnswerIndex] / botValues[botAnswerIndex]);

        answer = (float)Math.Round(answer, 3);

        answerTxt.text = answer.ToString();

        float piePercent = answer % 1;
        if (piePercent == 0)
        {
            piePercent = 1;
        }
        int pieAmount = (int)(answer - piePercent + 1);
        foreach (Image pie in pies)
        {
            pie.fillAmount = 0;
        }
        for (int i = 0; i < pieAmount; i++)
        {
            pies[i].fillAmount = 1;
        }
        pies[pieAmount - 1].fillAmount = piePercent;

        Vector3 fractionPieLocation = fractionPies.localPosition;
        switch (answer.ToString().Length)
        {
            case 1:
                fractionPieLocation.x = 188;
                break;
            /*case 2:
                fractionPieLocation.x = 243;
                break;*/
            case 3:
                fractionPieLocation.x = 250;
                break;
            case 4:
                fractionPieLocation.x = 290;
                break;
            default:
                fractionPieLocation.x = 330;
                break;
        }

        fractionPies.localPosition = fractionPieLocation;

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
