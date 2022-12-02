using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    public enum MathOperators
    {
        plus,
        min,
        times,
        divided
    }

    MathOperators mathOperators;

    void Start()
    {
        aTurn = true;
        bTurn = false;
        SetButtonValues();
        MakeSum();
        sumString = a + " " + mathOperator + " " + b + " = " + questionAnswer;
        sumText.text = sumString;
    }

    void Update()
    {
        CheckAnswer();
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
        UpdateAnswerText();
        UpdateSum();
    }

    public void SetOperators()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        mathOperator = clickedButton.name;
        UpdateAnswerText();
        UpdateSum();
    }

    public void SetButtonValues()
    {
        int rnd;
        int counter = 0;
        foreach (var button in numberButtons)
        {
            rnd = Random.Range(1, 11);
            button.GetComponent<SetButtonValue>().SetAllNumberValues(rnd);
            values.Add(rnd);
        }
        foreach (var button in operatorButtons)
        {
            string op = "";
            if (counter == 0)
            {
                op = "+";
                counter++;
            }
            else if (counter == 1)
            {
                op = "-";
                counter++;
            }
            else if (counter == 2)
            {
                op = "*";
                counter++;
            }
            else if (counter == 3)
            {
                op = "/";
                counter = 0;
            }
            button.GetComponent<SetButtonValue>().SetAllOperatorValues(op);
        }
    }

    public void UpdateAnswerText()
    {
        sumString = a + " " + mathOperator + " " + b + " = " + questionAnswer;
        sumText.text = sumString;
    }

    public float UpdateSumOperator()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (clickedButton.name == "+")
            return a + b;
        else if (clickedButton.name == "-")
            return a - b;
        else if (clickedButton.name == "*")
            return a * b;
        else if (clickedButton.name == "/")
            return a / b;
        else
            return 0;
    }

    public void UpdateSum()
    {
        sum = UpdateSumOperator();
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
                {
                    number2 = values[rnd];
                }
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
        bool yes = true;
        if (sum == questionAnswer)
        {
            if (yes) 
            {
                Debug.Log("Nailed it");
                yes = false;
            }
            
        }
    }

}
