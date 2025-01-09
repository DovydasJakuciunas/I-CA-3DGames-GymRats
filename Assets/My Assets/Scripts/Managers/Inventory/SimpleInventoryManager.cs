using GD.Items;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInventoryManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();

    public void OnInteractablePickUp(ItemData data)
    {
        if (inventory.ContainsKey(data))
        {
            inventory[data]++;
        }
        else
        {
            inventory.Add(data, 1);
        }
    }
}
