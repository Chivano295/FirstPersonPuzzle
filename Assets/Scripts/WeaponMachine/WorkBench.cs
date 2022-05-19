using System;
using System.Collections.Generic;

using UnityEngine;

public class WorkBench : MonoBehaviour
{
    public Recipe[] Recipes;
    public WorkbenchSlot[] Slots;
    public Pickup CurrentPickupInstance;

    public string[] Layout;

    private int callIdInternal;

    private void Awake()
    {
        //Discover recipes? and set them up
        foreach (var item in Slots)
        {
            item.Id = callIdInternal;
            callIdInternal++;
            item.PlayerPickup = CurrentPickupInstance;
            item.Callback = HandleOnItemStore;
        }
    }
    private void Update()
    {
    //Debug: allow crafting with U key
//#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.U))
        {
            AttemptRecipehandle();
        }
//#endif
    }
    //Check if the current inputed items have a recipe mapped
    //Private callback implementation
    private void HandleOnItemStore(WorkBenchCallbackContext context)
    {
        //Check if 
        if (context.Item != null)
        {
            foreach (Recipe recipe in Recipes)
            {
                CraftingRecipeMaterial[] recipeMaterials = new CraftingRecipeMaterial[3];
                for (int i = 0; i < Slots.Length; i++)
                {
                    if (context.CallerId == i)
                    {
                        recipeMaterials[i] = context.Item;

                    }
                }
            }
            AttemptRecipehandle();
        }
    }

    //Check if the current inputed items have a recipe mapped
    public void AttemptRecipehandle()
    {
        Recipe hitRecipe = null;
        CraftingRecipeMaterial[] recipeMaterials = new CraftingRecipeMaterial[Slots.Length];
        for (int i = 0; i < Slots.Length; i++)
        {
            recipeMaterials[i] = Slots[i].HeldMaterial;

        }
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.IsCompleted(recipeMaterials))
            {
                hitRecipe = recipe;
            }
        }
        if (hitRecipe != null)
        {
            Instantiate(hitRecipe.RecipeOut.gameObject, transform.position, Quaternion.identity);
            for (int i = 0; i < recipeMaterials.Length; i++)
            {
                Destroy(recipeMaterials[i].gameObject);
                recipeMaterials[i] = null;
            }
        }
    }
    //Class for holding values to be passed in callbacks on WorkBench
    public class WorkBenchCallbackContext
    {
        public int CallerId;
        public GameObject HoldingObject;
        public CraftingRecipeMaterial Item;
        public string ItemId;
    }
}