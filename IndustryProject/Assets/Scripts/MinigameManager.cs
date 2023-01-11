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
    public TextMeshProUGUI sumText;
    float a;
    float b;
    float questionAnswer;
    float sum;
    string sumString;
    string mathOperator = "";
    bool aTurn = true;
    bool bTurn = false;
    bool operatorVisible;
   
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
        GenerateQuiestiontype();
    }

    public void SetNumbers()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (aTurn)
        {
            a = float.Parse(clickedButton.name);
            if (operatorVisible)
            {
                SetTurbBoolToDefault(false, true);
            }
        }
        else if (bTurn && operatorVisible)
        {
            b = float.Parse(clickedButton.name);
            SetTurbBoolToDefault(true, false);
        }
        GetComponent<UIManager>().UpdateAnswerText(questionAnswer, a, b, mathOperator);
        UpdateSum();
    }

    public void SetOperatorsWithButton()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        mathOperator = clickedButton.name;
        GetComponent<UIManager>().UpdateAnswerText(questionAnswer, a, b, mathOperator);
        UpdateSum();
    }

    public void SetOperatorsAutomatic(int rnd)
    {
        if (rnd == 0)
            mathOperator = "+";
        else if (rnd == 1)
            mathOperator = "-";
        else if (rnd == 2)
            mathOperator = "x";
        else if (rnd == 3)
            mathOperator = ":";

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
            GetComponent<SumGeneration>().values.Add(rnd);
        }
        foreach (var button in operatorButtons)
        {
            string op = GetComponent<UIManager>().SetOperator(counter);
            button.GetComponent<SetButtonValue>().SetOperatorButtonValue(op);
            counter++;

            if (operatorVisible)
                button.SetActive(false);
            else
                button.SetActive(true);
        }
    }

    public void UpdateSum()
    {
        if (mathOperator == "+")
            sum = a + b;
        else if (mathOperator == "-")
            sum = a - b;
        else if (mathOperator == "x")
            sum = a * b;
        else if (mathOperator == ":")
            sum = a / b;
    }

    public void CheckAnswer()
    {
        if ((float)Math.Round(sum,2) == questionAnswer)
        { 
            Debug.Log("Nailed it");
            GenerateQuiestiontype();
        }
    }

    public void GenerateQuiestiontype()
    {
        int rnd = Random.Range(0, 2);

        if (rnd == 0)
            operatorVisible = true;
        else
            operatorVisible = false;
       

        if (operatorVisible)
            QuestionWithOperator();
        else
            QuestionWithoutOperator();
        
    }

    public void QuestionWithoutOperator()
    {
        SetTurbBoolToDefault(true, false);
        a = 0; 
        mathOperator = "";
        GetComponent<SumGeneration>().values.Clear();
        SetButtonValues();
        b = GetComponent<SumGeneration>().MakeSum(numberButtons.Length, Random.Range(0, 4));
        questionAnswer = GetComponent<SumGeneration>().answer;
        sumString = "? ? " + b + " = " + questionAnswer;
        sumText.text = sumString;
    }

    public void QuestionWithOperator()
    {
        SetTurbBoolToDefault(true, false);
        int rnd = Random.Range(0, 4);
        a = 0;
        b = 0;
        mathOperator = "";
        GetComponent<SumGeneration>().values.Clear();
        SetButtonValues();
        GetComponent<SumGeneration>().MakeSum(numberButtons.Length, rnd);
        SetOperatorsAutomatic(rnd);
        questionAnswer = GetComponent<SumGeneration>().answer;
        sumString = "? " + mathOperator +" ? " + " = " + questionAnswer;
        sumText.text = sumString;
    }

    private void SetTurbBoolToDefault(bool aTurn, bool bTurn)
    {
        this.aTurn = aTurn;
        this.bTurn = bTurn;
    }
}
