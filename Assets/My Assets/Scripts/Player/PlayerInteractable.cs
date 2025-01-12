using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer; // Define which objects are interactable  

    [SerializeField]
    private float interactableRange = 2f; // Radius of interaction

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Find all objects within the interactable range
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactableRange, interactableLayer);

        if (colliders.Length > 0) // If there are any objects in range
        {
            // Select the closest interactable object
            Collider closestCollider = null;
            float closestDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }

            // Interact with the closest object
            if (closestCollider != null)
            {
                Debug.Log("Interacting with " + closestCollider.name);

                var gymEquipment = closestCollider.GetComponent<GymEquipment>();
                if (gymEquipment != null)
                {
                    gymEquipment.Interact();
                }
            }
        }
        else
        {
            Debug.Log("No interactable objects in range.");
        }
    }
}
