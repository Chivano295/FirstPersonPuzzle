using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftingRecipeMaterial : MonoBehaviour
{
    public string id;

    public static bool operator ==(CraftingRecipeMaterial left, CraftingRecipeMaterial right)
    {
        if ((object)right == null)
            return (object)left == null;
        return left.id == right.id;
    }
    public static bool operator !=(CraftingRecipeMaterial left, CraftingRecipeMaterial right)
    {
        return !(left == right);
    }
}