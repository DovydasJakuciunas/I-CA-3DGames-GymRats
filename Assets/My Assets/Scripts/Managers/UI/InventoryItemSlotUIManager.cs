using UnityEngine;

public class InventoryItemSlotUIManager
{
    private Transform itemUIPanel;

    public InventoryItemSlotUIManager(Transform panel)
    {
        this.itemUIPanel = panel;
    }

    /// <summary>
    /// Adds an item UI to this panel.
    /// </summary>
    public GameObject AddItem(GameObject itemUIPrefab)
    {
        var itemUI = GameObject.Instantiate(itemUIPrefab, itemUIPanel);
        return itemUI;
    }
}
