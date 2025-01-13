using GD.Items;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotUIManager
{
    private Transform itemUIPanel;
    private GameObject selectedItemUI;
    private HashSet<GameObject> selectedItems = new HashSet<GameObject>();

    private GameObject spotLightItem;
    private GameObject itemDescriptionName;
    private GameObject itemDescription;

    public InventoryItemSlotUIManager(Transform panel, GameObject selectedItemUI, GameObject spotLightItem, GameObject itemDescriptionName, GameObject itemDescription)
    {
        this.itemUIPanel = panel;
        this.selectedItemUI = selectedItemUI;
        this.spotLightItem = spotLightItem;
        this.itemDescriptionName = itemDescriptionName;
        this.itemDescription = itemDescription;

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

        // Ensure a Button component exists and add a listener
        var button = itemUI.GetComponent<Button>();
        if (button == null)
        {
            button = itemUI.AddComponent<Button>();
        }

        // Add a click listener to toggle selection
        button.onClick.AddListener(() => ToggleSelection(itemUI));

        return itemUI;
    }

    /// <summary>
    /// Toggles the selection state of an item.
    /// </summary>
    private void ToggleSelection(GameObject itemUI)
    {
        SelectItem(itemUI);
    }

    /// <summary>
    /// Selects an item and distinguished it from a clone

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

        // Update the UI with details from the selected item
        var itemComponent = itemUI.GetComponent<Item>(); // Ensure the item prefab has the Item component
        if (itemComponent != null && itemComponent.itemData != null)
        {
            // Update spotlight sprite
            var spotLightImage = spotLightItem?.GetComponent<Image>();
            if (spotLightImage != null && itemComponent.itemData.UiIcon != null)
            {
                spotLightImage.sprite = itemComponent.itemData.UiIcon; // Set the item's icon
                spotLightItem.SetActive(true); // Ensure the spotlight is visible
            }

            // Update item name text
            var nameText = itemDescriptionName?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = itemComponent.itemData.name; // Set the item's name
            }

            // Update item description text
            var descriptionText = itemDescription?.GetComponent<TextMeshProUGUI>();
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
