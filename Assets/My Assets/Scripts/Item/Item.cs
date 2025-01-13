using GD.Events;
using UnityEngine;

// Code By Niall McGuinness
namespace GD.Items
{
    public class Item : MonoBehaviour, IConsumable
    {
        [SerializeField]
        [Tooltip("The item data that represents this item")]
        public ItemData itemData;

        [SerializeField]
        [Tooltip("The event that is raised when this item is consumed")]
        private ItemGameEvent onItemEvent;

        [SerializeField]
        [Tooltip("The layer that the item can be picked up by")]
        private LayerMask targetLayer;

        public string Description => itemData != null ? itemData.ItemType.ToString() : "No Description"; // Expose description

        public void Consume(GameObject consumer)
        {
            Debug.Log("Consuming item: " + itemData.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
            {
                if (itemData == null)
                {
                    return;
                }

                itemData.AudioPosition = transform.position;
                onItemEvent?.Raise(itemData);
                Destroy(gameObject);
                Debug.Log("Item Destroyed!");
            }
        }
    }
}
