using UnityEngine;

public class GymEquipment : MonoBehaviour
{
    [SerializeField]
    private MiniGame miniGamePrefab;

    public MiniGame GetMiniGamePrefab()
    {
        return miniGamePrefab;
    }
}
