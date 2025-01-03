using Sirenix.OdinInspector;
using System;
using UnityEngine;

//Used Niall McGuinness Code, https://github.com/nmcguinness/2024-25-GD3A-IntroToUnity/blob/4925c99ed50a84d14cf2764360d3a2936ce2be88/IntroToUnity/Assets/GD/Common/Scripts/Manager/Inventory/InventoryManager.cs

namespace GD.Items
{
    /// <summary>
    /// Manages the players inventory, listens for events, etc.
    /// </summary>
    /// <see cref="Inventory"/>
    /// <see cref="ItemData"/>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        [InlineEditor]
        [Tooltip("The player's inventory collection (e.g. a saddlebag")]
        private InventoryCollection inventoryCollection;

        private void Awake()
        {
            //check if the inventory collection has been added
            if (inventoryCollection == null)
            {
                throw new NullReferenceException("No inventory collection has been added");
            }
        }

        /// <summary>
        /// Adds the item to the inventory.
        /// </summary>
        /// <param name="data"></param>
        public void OnInventoryAdd(ItemData data)
        {
            Debug.Log("Adding item to inventory: " + data.name);

            inventoryCollection.Add(data);
        }
    }
}