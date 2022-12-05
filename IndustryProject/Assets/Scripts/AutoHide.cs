using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField] private float maxTime;

    private float timeLeft;

    private void OnEnable()
    {
        timeLeft = maxTime;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
