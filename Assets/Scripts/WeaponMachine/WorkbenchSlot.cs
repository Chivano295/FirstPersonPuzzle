using System;
using UnityEngine;

/// <summary>
/// A class for managing slots
/// </summary>
public class WorkbenchSlot : MonoBehaviour
{
    public WorkBench Registar;
    public Pickup PlayerPickup;
    public CraftingRecipeMaterial HeldMaterial;
    public Action<WorkBench.WorkBenchCallbackContext> Callback;
    public int Id;

    private void OnMouseDown()
    {
        //Create a context for the callback
        WorkBench.WorkBenchCallbackContext context = new WorkBench.WorkBenchCallbackContext();
        //Get the held object
        GameObject hold = PlayerPickup.Currentgrab;

        //Check if player is holding nothing if so return to prevent an exception
        if (hold == null) return;

        //Pass on some params
        context.HoldingObject = hold;
        context.CallerId = Id;

        //Get information about the held item
        if (hold.TryGetComponent<CraftingRecipeMaterial>(out CraftingRecipeMaterial recipeMaterial))
        {
            HeldMaterial = recipeMaterial;
            context.Item = recipeMaterial;
            context.ItemId = recipeMaterial.id;
        }
        else
        {
            context.Item = null;
        }
        //Drop the item and put it ontop of the crafting slot 
        PlayerPickup.Drop();
        hold.transform.position = transform.position + Vector3.up;
        hold.transform.localScale = Vector3.one * 0.8f;

        hold.tag = "Untagged";

        Callback(context);
    }
}