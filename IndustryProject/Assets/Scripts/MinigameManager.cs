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
using UnityEngine.Events;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] MinigameTransitionHandler minigameTransitionHandler;
    [SerializeField] GameObject[] numberButtons;
    [SerializeField] GameObject[] operatorButtons;
    public TextMeshProUGUI sumText;
    float a;
    float b;
    float questionAnswer;
    float sum;
    string sumString;
    string mathOperator = "";
    bool aTurn;
    bool bTurn;
    [SerializeField] int questionCount;
    private int correctQuestionCount = 0;
    /// the times table you want to generate from. if this is 1 the smallest times table will be 1.
    public int timesTableStart = 1;
    /// the times table you want to generate to. if this is 11 the biggest times table will be 10.
    public int timesTableEnd = 21;

    public UnityEvent onComplete;

    
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
            GetComponent<SumGeneration>().values.Add(rnd);
        }
        foreach (var button in operatorButtons)
        {
            string op = GetComponent<UIManager>().SetOperator(counter);
            button.GetComponent<SetButtonValue>().SetOperatorButtonValue(op);
            counter++;
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

    public void CheckAnswer()
    {
        if ((float)Math.Round(sum,2) == questionAnswer)
        {
            correctQuestionCount++;
            if(correctQuestionCount >= questionCount)
            {
                minigameTransitionHandler.RoomTransition(MinigameTransitionHandler.RoomType.sumPuzzle);               
                onComplete?.Invoke();
            }
            else
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        aTurn = true;
        bTurn = false;
        a = 0;
        b = 0;
        mathOperator = "";
        GetComponent<SumGeneration>().values.Clear();
        SetButtonValues();
        GetComponent<SumGeneration>().MakeSum(numberButtons.Length);
        questionAnswer = GetComponent<SumGeneration>().answer;
        sumString = " = " + questionAnswer;
        sumText.text = sumString;
    }
}
