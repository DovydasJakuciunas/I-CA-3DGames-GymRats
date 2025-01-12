using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    private float interactableRange = 2f; // Range to detect interactables

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Interact();
        }
    }

    public float getInteractableRange()
    {
        return interactableRange;
    }

    private void Interact()
    {
        // Find all objects within the interactable range
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactableRange, interactableLayer);

        if (colliders.Length > 0)
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
                var gymEquipment = closestCollider.GetComponent<GymEquipment>();
                if (gymEquipment != null)
                {
                    MiniGameManager.Instance.StartMiniGame(
                        gymEquipment.GetMiniGamePrefab(),
                        transform,
                        interactableRange
                    );
                }
            }
        }
        else
        {
            Debug.Log("No interactable objects in range.");
        }
    }
}
