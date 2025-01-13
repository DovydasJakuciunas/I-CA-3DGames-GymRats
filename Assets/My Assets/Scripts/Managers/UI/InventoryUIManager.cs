using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    #region Fields

    [SerializeField]
    [Tooltip("Title of the inventory panel")]
    [TextArea(2, 4)]
    private string description;

    [SerializeField]
    [Tooltip("Inventory to display for this UI panel")]
    private Inventory inventory;

    [SerializeField]
    [Tooltip("Panels to display inventory items in (one panel per new item)")]
    private List<Transform> itemUIPanels;

    [SerializeField]
    [Tooltip("Prefabs for inventory item UI (one prefab per new item)")]
    private List<GameObject> itemUIPrefabs;

    [SerializeField]
    private ItemData banana;

    #endregion Fields

    #region Fields - Internal

    private Dictionary<ItemData, GameObject> itemUIDictionary = new Dictionary<ItemData, GameObject>();
    private List<InventoryItemSlotUIManager> itemSlotManagers = new List<InventoryItemSlotUIManager>();
    private int currentPanelIndex = 0;
    private int currentPrefabIndex = 0;

    #endregion Fields - Internal

    #region Methods

    private void Start()
    {
        inventory.Clear();

        if (inventory == null)
        {
            throw new System.Exception("Inventory is not set in InventoryUIManager");
        }
        if (itemUIPanels == null || itemUIPanels.Count == 0)
        {
            throw new System.Exception("ItemUIPanels are not set in InventoryUIManager");
        }
        if (itemUIPrefabs == null || itemUIPrefabs.Count == 0)
        {
            throw new System.Exception("ItemUIPrefabs are not set in InventoryUIManager");
        }

        // Initialize slot managers for each panel
        foreach (var panel in itemUIPanels)
        {
            itemSlotManagers.Add(new InventoryItemSlotUIManager(panel));
        }

        InitializeUI();
    }

    private void InitializeUI()
    {
        foreach (var itemEntry in inventory.GetAllItems())
        {
            CreateOrUpdate(itemEntry.Key, itemEntry.Value);
        }
    }

    private void CreateOrUpdate(ItemData itemData, int count)
    {
        if (!itemUIDictionary.TryGetValue(itemData, out var itemUI))
        {
            // Assign the item to the next available panel
            InventoryItemSlotUIManager slotManager = itemSlotManagers[currentPanelIndex];
            var itemUIPrefab = itemUIPrefabs[currentPrefabIndex];

            itemUI = slotManager.AddItem(itemUIPrefab);
            if (itemUI == null)
            {
                Debug.LogError($"Failed to add item UI for {itemData.name}");
                return;
            }

            itemUI.SetActive(true);

            // Set item icon
            var iconImage = itemUI.GetComponentInChildren<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = itemData.UiIcon;
            }
            else
            {
                Debug.LogError($"Image component not found in prefab for {itemData.name}.");
            }

            // Add item UI to dictionary
            itemUIDictionary[itemData] = itemUI;

            // Move to the next panel and prefab, cycling back if necessary
            currentPanelIndex = (currentPanelIndex + 1) % itemSlotManagers.Count;
            currentPrefabIndex = (currentPrefabIndex + 1) % itemUIPrefabs.Count;
        }

        // Update item count text
        var countText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
        if (countText != null)
        {
            countText.text = count.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in item prefab.");
        }
    }

    /// <summary>
    /// Called when the inventory changes.
    /// Updates the UI dynamically to reflect inventory changes.
    /// </summary>
    public void OnInventoryChange()
    {
        foreach (var itemEntry in inventory.GetAllItems())
        {
            CreateOrUpdate(itemEntry.Key, itemEntry.Value);
        }
    }

    #endregion Methods
}
