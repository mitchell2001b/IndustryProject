using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] GameObject[] numberButtons;
    [SerializeField] GameObject[] operatorButtons;
    [SerializeField] public readonly List<int> values = new();
    public TextMeshProUGUI sumText;
    float a;
    float b;
    float questionAnswer;
    float sum;
    string sumString;
    string mathOperator = "";
    bool aTurn;
    bool bTurn;
   
    /// the times table you want to generate from. if this is 1 the smallest times table will be 1.
    public int timesTableStart = 1;
    /// the times table you want to generate to. if this is 11 the biggest times table will be 10.
    public int timesTableEnd = 21;

    public enum MathOperators
    {
        plus,
        min,
        times,
        divided
    }

    void Start()
    { 
        StartGame();
    }

    public void SetNumbers()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (aTurn)
        {
            a = float.Parse(clickedButton.name);
            aTurn = false;
            bTurn = true;
        }
        else if (bTurn)
        {
            b = float.Parse(clickedButton.name);
            aTurn = true;
            bTurn = false;
        }
        GetComponent<UIManager>().UpdateAnswerText(questionAnswer, a, b, mathOperator);
        UpdateSum();
    }

    public void SetOperators()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        mathOperator = clickedButton.name;
        GetComponent<UIManager>().UpdateAnswerText(questionAnswer, a, b, mathOperator);
        UpdateSum();
    }

    public void SetButtonValues()
    {
        int rnd;
        int counter = 0;
        foreach (var button in numberButtons)
        {
            rnd = Random.Range(timesTableStart, timesTableEnd);
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(rnd);
            values.Add(rnd);
        }
        foreach (var button in operatorButtons)
        {
            counter++;
            string op = GetComponent<UIManager>().SetOperator(counter);
            button.GetComponent<SetButtonValue>().SetOperatorButtonValue(op);
        }
    }

    public void UpdateSum()
    {
        if (mathOperator == "+")
            sum = a + b;
        else if (mathOperator == "-")
            sum = a - b;
        else if (mathOperator == "*")
            sum = a * b;
        else if (mathOperator == "/")
            sum = a / b;
    }

    public void MakeSum()
    {
        int rnd;
        int previousNumber = 0;
        float number1 = 0;
        float number2 = 0;
        for (int i = 0; i < 2; i++)
        {
            rnd = Random.Range(0, numberButtons.Length);
            if (i == 0)
            {
                number1 = values[rnd];
                previousNumber = rnd;
            }
            else if (i == 1)
            {
                if (previousNumber != rnd)
                    number2 = values[rnd];
                else
                {
                    while (previousNumber == rnd)
                    {
                        rnd = Random.Range(0, numberButtons.Length);
                        number2 = values[rnd];
                    }
                }
            }
        }
        MakeAnswer(number1, number2);
        Debug.Log(number1 + " " + number2);
    }

    public void MakeAnswer(float left, float right)
    {
        MathOperators mop = (MathOperators)Random.Range(0, 4);
        Debug.Log(mop);
        switch (mop)
        {
            case MathOperators.plus:
                questionAnswer = left + right;
                break;
            case MathOperators.min:
                questionAnswer = left - right;
                break;
            case MathOperators.times:
                questionAnswer = left * right;
                break;
            case MathOperators.divided:
                questionAnswer = left / right;
                break;
            default:
                break;
        }
    }

    public void CheckAnswer()
    {
        if ((float)Math.Round(sum,2) == questionAnswer)
        { 
            Debug.Log("Nailed it");
            StartGame();
        }
    }

    public void StartGame()
    {
        aTurn = true;
        bTurn = false;
        a = 0;
        b = 0;
        mathOperator = "";
        values.Clear();
        SetButtonValues();
        MakeSum();
        questionAnswer = (float)Math.Round(questionAnswer, 2);
        sumString = " = " + questionAnswer;
        sumText.text = sumString;
    }

}
