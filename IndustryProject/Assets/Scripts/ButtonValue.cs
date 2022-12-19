using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonValue : MonoBehaviour
{
    private int value;
    [SerializeField] private GameObject panelManager;
    
    private enum Type
    {
        Top,
        Bottom
    }

    private Type type;
    
    public void SendValue()
    {
        panelManager.GetComponent<FractionMinigameManager>().ReceiveValue(value, (int)type);
    }

    public void Init(int value, int type)
    {
        this.value = value;
        this.type = (Type)type;
    }
}
