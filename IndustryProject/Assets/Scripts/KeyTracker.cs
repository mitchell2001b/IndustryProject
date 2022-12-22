using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeyTracker : MonoBehaviour
{
    [SerializeField] private string keyWord;
    private int keyCount = 0;

    [SerializeField] private TextMeshProUGUI keyCounterTxt;

    [SerializeField] UnityEvent onComplete;
    [SerializeField] UnityEvent onAllKeysRetrieved;

    public void PickupKey()
    {
        keyCount++;
        UpdateText();
    }

    private void Awake()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        keyCounterTxt.text = string.Format("{0}: {1}", keyWord, keyCount);
    }

    public void TryOpenDoor()
    {
        if(keyCount == 2)
        {
            onComplete.Invoke();
        }
       
    }

    private void Update()
    {
        if(keyCount == 2)
        {
            onAllKeysRetrieved.Invoke();
        }
    }
}
