using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class WorkBenchNet : MonoBehaviour
{
    public Recipe[] Recipes;
    public WorkbenchSlotNet[] Slots;
    public Pickup CurrentPickupInstance;

    public string[] Layout;

    private int callIdInternal;

    private void Awake()
    {
        //Discover recipes?
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.U))
        {
            AttemptRecipehandle();
        }
#endif
    }

    private void HandleOnItemStore(WorkBenchCallbackContext context)
    {
        if (context.Item != null)
        {
            Recipe hitRecipe = null;
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
                if (recipe.IsCompleted(recipeMaterials))
                {
                    hitRecipe = recipe;
                }
            }
            if (hitRecipe != null)
            {
                Instantiate(hitRecipe.RecipeOut.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

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
        }
    }

    public class WorkBenchCallbackContext
    {
        public int CallerId;
        public GameObject HoldingObject;
        public CraftingRecipeMaterial Item;
        public string ItemId;
    }
}