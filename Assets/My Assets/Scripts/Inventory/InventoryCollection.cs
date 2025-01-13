using GD.Events;
using GD.Types;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Used Niall McGuinness Code, https://github.com/nmcguinness/2024-25-GD3A-IntroToUnity/blob/4925c99ed50a84d14cf2764360d3a2936ce2be88/IntroToUnity/Assets/GD/Common/Scripts/Inventory/InventoryCollection.cs

    /// <summary>
    /// Stores a dictionary of inventories
    /// </summary>
    /// <see cref="Inventory"/>
    /// <see cref="InventoryManager"/>
    [CreateAssetMenu(fileName = "InventoryCollection",
        menuName = "GD/Inventory/Collection")]
    public class InventoryCollection : SerializedScriptableObject
    {
        #region Fields

        [SerializeField]
        [Tooltip("A dictionary of all inventories (e.g. a saddlebag)")]
        private Dictionary<ItemCategoryType, Inventory> contents = new Dictionary<ItemCategoryType, Inventory>();

        [FoldoutGroup("Events")]
        [SerializeField]
        [Tooltip("Event to raise when the collection changes.")]
        private GameEvent onCollectionChange;

        [FoldoutGroup("Events")]
        [SerializeField]
        [Tooltip("Event to raise when the collection is emptied.")]
        private GameEvent onCollectionEmpty;

        #endregion Fields

        #region Properties

        public Inventory this[ItemCategoryType categoryType]
        {
            get
            {
                return contents[categoryType];
            }
        }

        #endregion Properties

        public Inventory Get(ItemCategoryType itemCategory)
        {
            if (!contents.ContainsKey(itemCategory))
                throw new NullReferenceException("No inventory for this item category");

            return contents[itemCategory];
        }

        //add a new inventory to the collection
        public void Add(ItemData itemData, int quantity = 1)
        {
            // If the inventory for the item category does not exist, create it
            if (!contents.ContainsKey(itemData.ItemCategory))
            {
                contents[itemData.ItemCategory] = new Inventory();
            }

            // Add the specified quantity of the item to the inventory
            contents[itemData.ItemCategory].Add(itemData, quantity);

            // Tell interested parties that the collection has changed
            onCollectionChange?.Raise();
        }

        /// <summary>
        /// Removes all inventories from the collection.
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            contents.Clear();
            onCollectionEmpty?.Raise();
            return contents.Count == 0;
        }

        // Removes an inventory from the collection
        public void Remove(Inventory inventory)
        {
            // Find the key associated with the inventory
            var key = contents.FirstOrDefault(x => x.Value == inventory).Key;

            // If the key is found, remove the inventory
            if (!EqualityComparer<ItemCategoryType>.Default.Equals(key, default))
            {
                contents.Remove(key); //remove inventory 
                onCollectionChange?.Raise(); //notify subscribers 
            }
        }

    /// <summary>
    /// Removes one count of the specified item from the collection.
    /// </summary>
    /// <param name="itemData">The item to remove.</param>
    /// <param name="quantity">The quantity to remove (default is 1).</param>
    /// <returns>True if the item was successfully removed, false otherwise.</returns>
    public bool RemoveOne(ItemData itemData, int quantity = 1)
    {
        // Check if the item category exists in the collection
        if (!contents.ContainsKey(itemData.ItemCategory))
        {
            Debug.LogWarning("No inventory exists for the specified item category.");
            return false;
        }

        // Get the inventory for the specified category
        var inventory = contents[itemData.ItemCategory];

        // Check if the inventory contains the item
        if (!inventory.Contains(itemData))
        {
            Debug.LogWarning("The specified item does not exist in the inventory.");
            return false;
        }

        // Get the current count of the item
        int currentCount = inventory.Count(itemData);

        // If the current count is less than or equal to the quantity to remove
        if (currentCount <= quantity)
        {
            // Remove the item entirely
            inventory.Remove(itemData,1);

            // If the inventory is now empty, remove it from the collection
            if (inventory.isEmpty())
            {
                Remove(inventory);
            }

            Debug.Log($"Item '{itemData.name}' fully removed from inventory.");
        }
        else
        {
            // Decrement the item count
            inventory.Add(itemData, -quantity);
            Debug.Log($"Removed {quantity} of item '{itemData.name}'. Remaining count: {currentCount - quantity}.");
        }

        // Notify listeners that the collection has changed
        onCollectionChange?.Raise();

        return true;
    }

}
