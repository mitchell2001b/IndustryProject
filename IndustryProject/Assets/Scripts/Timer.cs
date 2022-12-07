using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Time (in seconds):")]
    [SerializeField] private float time = 70; // the maximum time, which the timer gets reset to if you reset the game

    private bool timerOn; // to turn off the timer when the time reaches 0

    private float timeLeft; // how much time is left

    [Header("Timer TextMeshPro object")]
    [SerializeField] private TextMeshProUGUI timerTxt; // the TextMeshPro object that shows the user how much time is left

    [Header("What happens when the timer ends")]
    [SerializeField] private UnityEvent onTimerEnd; // the event that gets called when the timer reaches 0

    private void Awake()
    {
        Reset(); // reset timeLeft and timerOn
    }

    private void FixedUpdate()
    {
        if (timerOn == true)
        {
            if (timeLeft > 0) // if there is time left
            {
                timeLeft -= Time.deltaTime; // decrease timeLeft
                UpdateTimer(timeLeft); // update timerTxt.text with the correct amount of remaining time
            }
            else // if there is no more time left
            {
                onTimerEnd?.Invoke(); // call onTimerEnd event
                timeLeft = 0; // make sure timeLeft equals 0 and isn't less than zero
                timerOn = false; // disable timer
            }
        }
    }

    private void UpdateTimer(float currentTime) // update timerTxt.text with the correct amount of remaining time
    {
        currentTime += 1;
        
        float minutes = Mathf.FloorToInt(currentTime / 60); // calculate minutes by dividing currentTime by 60 and rounding down
        float seconds = Mathf.FloorToInt(currentTime % 60); // calculate seconds by calculating (currentTime)mod(60)

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds); // set timerTxt.text
    }

    // reset timeLeft to maximum given time and timerOn to true
    public void Reset()
    {
        timeLeft = time;
        timerOn = true;
    }
}
