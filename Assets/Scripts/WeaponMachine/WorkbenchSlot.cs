using System;
using UnityEngine;

public class WorkbenchSlot : MonoBehaviour
{
    public WorkBench Registar;
    public Pickup PlayerPickup;
    public CraftingRecipeMaterial HeldMaterial;
    public Action<WorkBench.WorkBenchCallbackContext> Callback;
    public int Id;

    private void OnMouseDown()
    {
        WorkBench.WorkBenchCallbackContext context = new WorkBench.WorkBenchCallbackContext();
        GameObject hold = PlayerPickup.Currentgrab;
        context.HoldingObject = hold;
        context.CallerId = Id;
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
        Callback(context);
    }
}