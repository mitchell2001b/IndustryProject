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
    [SerializeField] PlaySound sound;
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
        List<float> numbers = GetComponent<SumGeneration>().values;
        int counter = 0;
        foreach (var button in numberButtons)
        {
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(numbers[counter]);
            counter++;
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
        Debug.Log(a + "dit is a" + b + "dit is b");
        if (mathOperator == "+")
            sum = a + b;
        else if (mathOperator == "-")
            sum = a - b;
        else if (mathOperator == "x")
            sum = a * b;
        else if (mathOperator == ":")
            sum = a / b;

        Debug.Log(sum + "update");
        
    }

    public void CheckAnswer()
    {
        Debug.Log("lets check");
        if ((float)Math.Round(sum,2) == questionAnswer)
        {
            
            sound.playButton();
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
        else
        {
            
            Debug.Log("wrong");
        }
    }

    public void StartGame()
    {
        aTurn = true;
        bTurn = false;
        a = 0;
        b = 0;
      
        GetComponent<SumGeneration>().values.Clear();
        //GetComponent<SumGeneration>().MakeSum(numberButtons.Length);
        GetComponent<SumGeneration>().GenerateSum(numberButtons.Length);
        questionAnswer = GetComponent<SumGeneration>().answer;
        mathOperator = GetComponent<SumGeneration>().correctOperator;
        SetButtonValues();
        sumString = " ? " + mathOperator + " ? = " + questionAnswer;
        sumText.text = sumString;
    }
}
