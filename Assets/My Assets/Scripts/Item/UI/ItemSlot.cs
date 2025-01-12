using GD.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCount;

    public void SetItem(ItemData itemData, int count)
    {
        itemIcon.sprite = itemData.UiIcon;
        itemName.text = itemData.Name;
        itemCount.text = count.ToString();

    }
}
