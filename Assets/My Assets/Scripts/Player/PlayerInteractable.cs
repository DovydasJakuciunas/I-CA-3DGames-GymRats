using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    private float interactableRange = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactableRange, interactableLayer))
        {
            Debug.Log("Interacting with " + hit.collider.name);

            var gymEquipment = hit.collider.GetComponent<GymEquipment>();
            if (gymEquipment != null)
            {
                gymEquipment.Interact();
            }
        }
    }
}


