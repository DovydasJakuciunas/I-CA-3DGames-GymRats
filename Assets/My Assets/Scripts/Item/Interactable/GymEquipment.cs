using UnityEngine;

public class GymEquipment : MonoBehaviour
{
    [SerializeField]
    private string equipmentName; // The name of this gym equipment, displayed in the mini-game UI.

    [SerializeField]
    private MiniGame miniGamePrefab; // The mini-game prefab associated with this gym equipment.

    /// <summary>
    /// Method to interact with the gym equipment.
    /// This triggers the mini-game UI and starts the corresponding mini-game.
    /// </summary>
    public void Interact()
    {
        // Display the mini-game UI with the equipment name as the title.
        if (MiniGameUIManager.Instance != null)
        {
            MiniGameUIManager.Instance.ShowMiniGameUI(equipmentName);
        }

        // Start the associated mini-game.
        if (MiniGameManager.Instance != null)
        {
            MiniGameManager.Instance.StartMiniGame(miniGamePrefab);
        }
        else
        {
            Debug.LogError("MiniGameManager instance is not found in the scene.");
        }

        Debug.Log($"Interacting with {equipmentName}");
    }
}
