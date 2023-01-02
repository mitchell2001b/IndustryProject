using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniPuzzleSumScene : MonoBehaviour
{
    private int ingredientCount;

    private bool ingredientCheck = false;

    [SerializeField] UnityEvent onComplete;

    [SerializeField] int ingredientAmount;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddIngredient()
    {
        ingredientCount++;
        if(CheckIfIngredientTotal())
        {
            onComplete.Invoke();
        }
    }

    private bool CheckIfIngredientTotal()
    {
        if(ingredientCount >= ingredientAmount)
        {
            ingredientCheck = true;
        }

        return ingredientCheck;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
