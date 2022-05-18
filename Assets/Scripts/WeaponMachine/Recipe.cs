using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe01", menuName = "Custom/Craftable recipe", order = 0)]
public class Recipe : ScriptableObject
{
    public CraftingRecipeMaterial[] IntakeObjects;
    public CraftingRecipeMaterial RecipeOut;
    public int OutAmount;

    public Recipe(CraftingRecipeMaterial[] blockIn, CraftingRecipeMaterial blockOut, int craftAmount)
    {
        this.IntakeObjects = blockIn;
        this.RecipeOut = blockOut;
        this.OutAmount = craftAmount;
    }

    /// <summary>
    /// Determines of a Recipe's intake objects match the given array
    /// </summary>
    /// <param name="crm"></param>
    /// <returns></returns>
    public bool IsCompleted(CraftingRecipeMaterial[] crm)
    {
        if (crm.Length < IntakeObjects.Length)
            return false;

        for (int i = 0; i < IntakeObjects.Length; i++)
        {
            if (IntakeObjects[i] != crm[i])
                return false;
        }
        return true;
    }
    
}
