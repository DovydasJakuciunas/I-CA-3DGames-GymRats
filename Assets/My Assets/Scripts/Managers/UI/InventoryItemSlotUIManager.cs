using GD.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlotUIManager
{
    private Transform itemUIPanel;
    private GameObject selectedItemUI;
    private GameObject spotLightItem;
    private GameObject itemDescriptionName;
    private GameObject itemDescription;
    private GameObject player; // Reference to the player
    private Inventory inventoryCollection; // Reference to the inventory collection

    public InventoryItemSlotUIManager(Transform panel, GameObject selectedItemUI, GameObject spotLightItem, GameObject itemDescriptionName, GameObject itemDescription, GameObject player, Inventory inventoryCollection)
    {
        this.itemUIPanel = panel;
        this.selectedItemUI = selectedItemUI;
        this.spotLightItem = spotLightItem;
        this.itemDescriptionName = itemDescriptionName;
        this.itemDescription = itemDescription;
        this.player = player;
        this.inventoryCollection = inventoryCollection;

        if (selectedItemUI != null)
        {
            selectedItemUI.SetActive(false); // Ensure the selection UI is initially disabled
        }
    }

    /// <summary>
    /// Adds an item UI to this panel.
    /// </summary>
    public GameObject AddItem(GameObject itemUIPrefab)
    {
        GameObject itemUI = Object.Instantiate(itemUIPrefab, itemUIPanel);

        // Ensure a Button component exists and add a listener for left-click
        Button button = itemUI.GetComponent<Button>();
        if (button == null)
        {
            button = itemUI.AddComponent<Button>();
        }

        button.onClick.AddListener(() => ToggleSelection(itemUI)); // Handle left-click for selection

        // Add an EventTrigger to detect right-click
        EventTrigger eventTrigger = itemUI.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = itemUI.AddComponent<EventTrigger>();
        }

        // Add a new entry for right-click
        EventTrigger.Entry rightClickEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        rightClickEntry.callback.AddListener((eventData) =>
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            if (pointerEventData != null && pointerEventData.button == PointerEventData.InputButton.Right)
            {
                ConsumeItem(itemUI); // Call ConsumeItem when right-clicked
            }
        });

        eventTrigger.triggers.Add(rightClickEntry);

        return itemUI;
    }

    /// <summary>
    /// Consumes the specified item and removes one count from the inventory.
    /// </summary>
    private void ConsumeItem(GameObject itemUI)
    {
        Item itemComponent = itemUI.GetComponent<Item>();
        if (itemComponent != null && itemComponent.itemData != null)
        {
            // Call the Consume method
            itemComponent.Consume(player);

            // Remove one count of the item from the inventory
            int remainingCount = inventoryCollection.Remove(itemComponent.itemData, 1);

            // Update the UI or destroy the item if the count is 0
            TextMeshProUGUI countText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
            if (countText != null)
            {
                if (remainingCount > 0)
                {
                    countText.text = remainingCount.ToString();
                }
                else
                {
                    // If the item count reaches 0, remove the item UI
                    Object.Destroy(itemUI);
                }
            }
        }
        else
        {
            Debug.LogError("Item component or ItemData is missing on the item UI.");
        }
    }

    /// <summary>
    /// Toggles the selection state of an item.
    /// </summary>
    private void ToggleSelection(GameObject itemUI)
    {
        SelectItem(itemUI);
    }

    /// <summary>
    /// Selects an item and updates the UI details for it.
    /// </summary>
    private void SelectItem(GameObject itemUI)
    {
        if (selectedItemUI == null)
        {
            Debug.LogError("Selected Item UI is not assigned in the Inspector.");
            return;
        }

        selectedItemUI.SetActive(true); // Activate the selection box

        // Reparent the selectedItemUI to the parent of the clicked item
        selectedItemUI.transform.SetParent(itemUI.transform.parent, false);

        // Dynamically position the selectedItemUI to match the clicked item's position
        RectTransform itemRectTransform = itemUI.GetComponent<RectTransform>();
        RectTransform selectedUIRectTransform = selectedItemUI.GetComponent<RectTransform>();

        if (itemRectTransform != null && selectedUIRectTransform != null)
        {
            selectedUIRectTransform.anchoredPosition = itemRectTransform.anchoredPosition;
            selectedUIRectTransform.SetSiblingIndex(itemRectTransform.GetSiblingIndex() - 1);
        }

        UpdateRightSide(itemUI);
    }

    /// <summary>
    /// Updates the spotlight, name, and description UI for the selected item.
    /// </summary>
    private void UpdateRightSide(GameObject itemUI)
    {
        Item itemComponent = itemUI.GetComponent<Item>(); // Ensure the item prefab has the Item component
        if (itemComponent != null && itemComponent.itemData != null)
        {
            // Update spotlight sprite
            Image spotLightImage = spotLightItem?.GetComponent<Image>();
            if (spotLightImage != null && itemComponent.itemData.UiIcon != null)
            {
                spotLightImage.sprite = itemComponent.itemData.UiIcon; // Set the item's icon
                spotLightItem.SetActive(true); // Ensure the spotlight is visible
            }

            // Update item name text
            TextMeshProUGUI nameText = itemDescriptionName?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = itemComponent.itemData.name; // Set the item's name
            }

            // Update item description text
            TextMeshProUGUI descriptionText = itemDescription?.GetComponent<TextMeshProUGUI>();
            if (descriptionText != null)
            {
                descriptionText.text = itemComponent.itemData.Description; // Set the item's description
            }
        }
        else
        {
            Debug.LogError("Item component or ItemData is missing on the selected item UI.");
        }
    }
}
