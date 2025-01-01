using GD.Events;
using UnityEngine;

namespace GD.Items
{
    /// <summary>
    /// Represents an item that can be consumed by a game object on the correct layer
    /// </summary>
    /// <see cref="ItemData"/>
    /// <see cref="ItemGameEvent"/>
    public class Item : MonoBehaviour, IConsumable
    {
        [SerializeField]
        [Tooltip("The item data that represents this item")]
        private ItemData itemData;

        [SerializeField]
        [Tooltip("The event that is raised when this item is consumed")]
        private ItemGameEvent onItemEvent;

        [SerializeField]
        [Tooltip("The layer that the item can be picked up by")]
        private LayerMask targetLayer;

        /// <summary>
        /// Consumes the item
        /// </summary>
        /// <param name="consumer">Reference to consuming object</param>
        public void Consume(GameObject consumer)
        {
            Debug.Log("Consuming item: " + itemData.name);
        }

        /// <summary>
        /// Called when the item is picked up by a game object on the target layer
        /// </summary>
        /// <param name="other">Reference to collider object triggering response</param>
        private void OnTriggerEnter(Collider other)
        {
            // Check if the other object's layer is in the targetLayer
            if (((1 << other.gameObject.layer) & targetLayer.value) != 0)
            {
                if (itemData == null)
                {
                    return;
                }

                // Set the audio position to the transform position
                itemData.AudioPosition = transform.position;

                // Raise the event to notify listeners
                onItemEvent?.Raise(itemData);

                // Remove the item from the scene
                Destroy(gameObject);
            }
        }

    }
}