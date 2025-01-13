using GD;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

//Used Niall McGuinness Code, https://github.com/nmcguinness/2024-25-GD3A-IntroToUnity/blob/4925c99ed50a84d14cf2764360d3a2936ce2be88/IntroToUnity/Assets/GD/Common/Scripts/Manager/Inventory/InventoryManager.cs
//Refactored a bit with this youtube tutorial: https://www.youtube.com/watch?v=LaQp5u0_UYk&list=PLSR2vNOypvs6eIxvTu-rYjw2Eyw57nZrU&index=2


/// <summary>
/// Manages the player's inventory, listens for events, etc.
/// </summary>
/// <see cref="Inventory"/>
/// <see cref="ItemData"/>
public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField]
    [InlineEditor]
    [Tooltip("The player's inventory collection (e.g. a saddlebag)")]
    private InventoryCollection inventoryCollection;

    [SerializeField]
    [Tooltip("GameObject so that it knows what to call")]
    private GameObject InventoryMenu;

    // Checking if the menu is activated
    private bool menuActivated;

    protected override void Awake()
    {
        base.Awake(); // Call Singleton's Awake to handle instance initialization


        // Check if the inventory collection has been added
        if (inventoryCollection == null)
        {
            throw new NullReferenceException("No inventory collection has been added");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    /// <summary>
    /// Called when an interactable item is picked up.
    /// </summary>
    /// <param name="data">The item data to add to the inventory.</param>
    public void OnInteractablePickup(ItemData data)
    {
        inventoryCollection.Add(data);
    }

    /// <summary>
    /// Adds the item to the inventory.
    /// </summary>
    /// <param name="data">The item data to add to the inventory.</param>
    public void OnInventoryAdd(ItemData data)
    {
        Debug.Log("Adding item to inventory: " + data.name);
        inventoryCollection.Add(data);
    }
}
