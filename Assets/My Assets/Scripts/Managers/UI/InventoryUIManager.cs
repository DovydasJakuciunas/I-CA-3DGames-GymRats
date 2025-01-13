using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private AudioSource audioSource;    //To play all types of dialogue based on the items name 

    /*Explanation
     * Similar working as UI script, on our inventory gameobject in our canvas we put gameEventListener where we put in scriptableobject game event:  InventoryChangeEvent 
     * and create response with dragging the inventory object there and adding InventoryUIManager script and selecting OnInventoryChange method
     * This will update inventory UI when inventory changes
     * Then in playerInput script we set the toggle on or off based on pressing key 
     */

    private void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogWarning("Inventory not assigned.");
        }

        // Hide the inventory panel at the start
        inventoryPanel.SetActive(false);
        descriptionPanel.SetActive(false);
        UpdateInventoryUI();
        audioSource = GetComponent<AudioSource>();

    }


    public void OnInventoryChange()
    {
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        // Clear existing slots to avoid duplicates
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // Iterate through player's inventory items 
        foreach (KeyValuePair<ItemData, int> item in playerInventory)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer); // Instantiate the item slot prefab
            ItemSlot itemSlotUI = itemSlot.GetComponent<ItemSlot>(); // Get the item slot UI component
            itemSlotUI.SetItem(item.Key, item.Value); // Set the item slot UI with the item and how many are in the inventory

            Button button = itemSlot.GetComponent<Button>(); // Get the button component of the item slot
            if (button != null)
            {
                button.onClick.AddListener(() => OnItemClick(item.Key)); // Add a listener to the button to call OnItemClick when clicked
            }
        }
    }

    public void ToggleInventoryPanel()
    {
        bool inInventory = !inventoryPanel.activeSelf; // Check if the inventory panel is active or not
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        UpdateInventoryUI(); // Update the inventory UI when the panel is viewed

        if (!inInventory)
        {
            descriptionPanel.SetActive(false); // If we close the inventory panel then close the description panel
        }
    }

    public void OnItemClick(ItemData item) // When we click on item (button) then show the panel
    {
        if (descriptionPanel != null && descriptionText != null)
        {
            descriptionText.text = item.Description; // Set the description text to the item description
            descriptionPanel.SetActive(true); // Show the description panel
        }
    }

    public bool IsInventoryPanelActive() // Check for in inventory state 
    {
        return inventoryPanel.activeSelf;
    }
}