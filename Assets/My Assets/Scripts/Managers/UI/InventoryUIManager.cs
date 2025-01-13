using GD.Items;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    #region Fields

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
    [Tooltip("Selected Item UI for displaying item details")]
    private GameObject selectedItemUI;

    [SerializeField]
    [Tooltip("The inventory item in hierarchy")]
    private GameObject inventoryItem;

    #endregion Fields

    [SerializeField]
    [Tooltip("Item Highlight on the right")]
    private GameObject spotLightItem;

    [SerializeField]
    [Tooltip("Name of the highlighted item")]
    private GameObject itemDescriptionName;

    [SerializeField]
    [Tooltip("Description of the highlighted item")]
    private GameObject itemDescription;

    #region Fields - Internal

    private Dictionary<ItemData, GameObject> itemUIDictionary = new Dictionary<ItemData, GameObject>();
    private List<InventoryItemSlotUIManager> itemSlotManagers = new List<InventoryItemSlotUIManager>();

    #endregion Fields - Internal

    #region Methods

    private void Start()
    {
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
        for (int i = 0; i < itemUIPanels.Count; i++)
        {
            InventoryItemSlotUIManager slotManager = new InventoryItemSlotUIManager(
                itemUIPanels[i],
                selectedItemUI,
                spotLightItem,
                itemDescriptionName,
                itemDescription
            );
            itemSlotManagers.Add(slotManager);
        }

        inventory.Clear();

        InitializeUI();
        inventoryItem.SetActive(false);
    }

    private void InitializeUI()
    {
        foreach (KeyValuePair<ItemData, int> itemEntry in inventory.GetAllItems())
        {
            CreateOrUpdate(itemEntry.Key, itemEntry.Value);
        }
    }

    private void CreateOrUpdate(ItemData itemData, int count)
    {
        if (itemUIDictionary.TryGetValue(itemData, out GameObject itemUI))
        {
            if (itemUI == null)
            {
                // The item UI has been destroyed, remove it from the dictionary
                itemUIDictionary.Remove(itemData);
            }
            else
            {
                UpdateCount(count, itemUI);
                return;
            }
        }

        // Create a new item UI if it doesn't exist or was destroyed
        InventoryItemSlotUIManager slotManager = itemSlotManagers[itemUIDictionary.Count % itemSlotManagers.Count];
        GameObject itemUIPrefab = itemUIPrefabs[itemUIDictionary.Count % itemUIPrefabs.Count];

        itemUI = slotManager.AddItem(itemUIPrefab);
        if (itemUI == null)
        {
            Debug.LogError($"Failed to add item UI for {itemData.name}");
            return;
        }

        itemUI.SetActive(true);

        Image iconImage = itemUI.GetComponentInChildren<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = itemData.UiIcon;
        }
        else
        {
            Debug.LogError($"Image component not found in prefab for {itemData.name}.");
        }

        // Attach Item component and assign ItemData
        Item itemComponent = itemUI.AddComponent<Item>();
        itemComponent.itemData = itemData;

        itemUIDictionary[itemData] = itemUI;
    }


    private static void UpdateCount(int count, GameObject itemUI)
    {
        // Update item count text
        TextMeshProUGUI countText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
        if (countText != null)
        {
            countText.text = count.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in item prefab.");
        }
    }

    public void OnInventoryChange()
    {
        foreach (KeyValuePair<ItemData, int> itemEntry in inventory.GetAllItems())
        {
            CreateOrUpdate(itemEntry.Key, itemEntry.Value);
        }
    }

    #endregion Methods
}
