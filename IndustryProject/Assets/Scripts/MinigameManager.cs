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
    private bool operatorVisible;

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
                SetTurbBool(false, true);
            }
        }
        else if (bTurn)
        {
            b = float.Parse(clickedButton.name);
            SetTurbBool(true, false);
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
        int counterNumbers = 0;
        int counterOperators = 0;
        foreach (var button in numberButtons)
        {
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(numbers[counterNumbers]);
            counterNumbers++;
        }
        foreach (var button in operatorButtons)
        {
            string op = GetComponent<UIManager>().SetOperator(counterOperators);
            button.GetComponent<SetButtonValue>().SetOperatorButtonValue(op);
            counterOperators++;

            if (operatorVisible)
                button.SetActive(false);
            else
                button.SetActive(true);
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
        if (sum == questionAnswer)
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
                GenerateQuiestiontype();
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
        SetTurbBool(true, false);
        a = 0;
        mathOperator = "";
        GetComponent<SumGeneration>().values.Clear();
        GetComponent<SumGeneration>().GenerateSum(numberButtons.Length);
        b = GetComponent<SumGeneration>().inputB;
        questionAnswer = GetComponent<SumGeneration>().answer;
        SetButtonValues();
        sumString = "? ? " + b + " = " + questionAnswer;
        sumText.text = sumString;
    }

    public void QuestionWithOperator()
    {
        SetTurbBool(true, false);
        aTurn = true;
        bTurn = false;
        a = 0;
        b = 0;

        mathOperator = "";
        GetComponent<SumGeneration>().values.Clear();
        GetComponent<SumGeneration>().GenerateSum(numberButtons.Length);
        mathOperator = GetComponent<SumGeneration>().correctOperator;
        questionAnswer = GetComponent<SumGeneration>().answer;
        SetButtonValues();
        sumString = "? " + mathOperator + " ?  = " + questionAnswer;
        sumText.text = sumString;
    }

    private void SetTurbBool(bool aTurn, bool bTurn)
    {
        this.aTurn = aTurn;
        this.bTurn = bTurn;
    }
}
