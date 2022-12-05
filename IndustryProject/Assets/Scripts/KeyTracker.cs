using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyTracker : MonoBehaviour
{
    [SerializeField] private string keyWord;
    private int keyCount = 0;

    [SerializeField] private TextMeshProUGUI keyCounterTxt;

    [SerializeField] private GameObject tryOpenDoor;

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
        tryOpenDoor.GetComponent<OpenDoor>().tryOpenDoor?.Invoke(keyCount);
    }
}
