using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    #region Fields

    [Tooltip("UI to show when player is in range.")]
    [SerializeField]
    private GameObject interactableUI;

    [Tooltip("Corrosponding image to the workout")]
    [SerializeField]
    private GameObject workoutImage;

    #endregion
    #region Interactable Funcionality
    private void Awake()
    {
        interactableUI.SetActive(false); //make sure not to display when start the game
        workoutImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //when player gets in range display the UI 
        {
            if (interactableUI != null)
            {
                interactableUI.SetActive(true);
                workoutImage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactableUI != null)
        {
            interactableUI.SetActive(false);
            workoutImage.SetActive(false);
        }
    }

    #endregion
}
