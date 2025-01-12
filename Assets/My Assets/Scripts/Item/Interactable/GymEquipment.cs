using UnityEngine;

public class GymEquipment : MonoBehaviour
{
    [SerializeField]
    private string equipmentName;

    [SerializeField]
    private MiniGame miniGamePrefab;

    public void Interact()
    {
        Debug.Log($"Player started using {equipmentName}");

        MiniGameManager.Instance.StartMiniGame(miniGamePrefab);
    }
}
