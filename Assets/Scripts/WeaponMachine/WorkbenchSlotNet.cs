using System;
using UnityEngine;

public class WorkbenchSlotNet : MonoBehaviour
{
    public WorkBench Registar;
    public Pickup PlayerPickup;
    public CraftingRecipeMaterial HeldMaterial;
    public Action<WorkBenchNet.WorkBenchCallbackContext> Callback;
    public int Id;

    private void OnMouseDown()
    {
        WorkBenchNet.WorkBenchCallbackContext context = new WorkBenchNet.WorkBenchCallbackContext();
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