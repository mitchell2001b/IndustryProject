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
    [SerializeField] int totalKeyCount;

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
        //keyCounterTxt.text = string.Format("{0}: {1}", keyWord, keyCount);
        keyCounterTxt.text = string.Format(keyCount.ToString());
    }

    public void TryOpenDoor()
    {
        if(keyCount == totalKeyCount)
        {
            onComplete.Invoke();
        }
       
    }

    private void Update()
    {
        if(keyCount == totalKeyCount)
        {
            onAllKeysRetrieved.Invoke();
        }
    }
}
