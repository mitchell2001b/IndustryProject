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

public class FractionMinigameManager : MonoBehaviour
{
    [SerializeField] private FractionGenerator fractionGenerator;
    [SerializeField] int questionCount;
    [SerializeField] UnityEvent onComplete;
    private int score;

    private float topAnswer = 0;
    private float botAnswer = 0;

    private float answer = 0;

    [SerializeField] private GameObject timer;

    [SerializeField] private int wrongAnswerPenalty = 20;
    [SerializeField] PlaySound sound;

    private void Start()
    {
        answer = fractionGenerator.Generate();
        GetComponent<UIManager>().SetFractionText(questionCount, score);
    }

    public void ReceiveValue(int value, int type)
    {
        if (type == 0)
        {
            topAnswer = value;
        }
        else
        {
            botAnswer = value;
        }

        if (topAnswer != 0 && botAnswer != 0)
        {
            CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        float givenAnswer = topAnswer/ botAnswer;
        givenAnswer = (float)Math.Round(givenAnswer, 2);
        if (givenAnswer == answer)
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }
    }

    private void IncorrectAnswer()
    {
        timer.GetComponent<Timer>().DecreaseTimer(wrongAnswerPenalty);
        ResetValues();
    }

    private void CorrectAnswer()
    {
        score++;
        sound.playButton();
        answer = fractionGenerator.Generate();
        if (score >= questionCount)
        {
            onComplete.Invoke();
        }
        ResetValues();
        
    }

    private void ResetValues()
    {
        GetComponent<UIManager>().SetFractionText(questionCount, score);
        topAnswer = 0;
        botAnswer = 0;
    }
}
