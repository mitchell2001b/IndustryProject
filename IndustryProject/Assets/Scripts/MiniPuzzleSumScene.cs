using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuzzleSumScene : MonoBehaviour
{
    private int ingredientCount;

    private bool ingredientCheck = false;

    [SerializeField] GameObject emptyGlass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddIngredient()
    {
        ingredientCount++;
        if(CheckIfIngredientTotal())
        {
            emptyGlass.SetActive(true);
        }
    }

    private bool CheckIfIngredientTotal()
    {
        if(ingredientCount >= 2)
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
