using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartMiniGame(MiniGame miniGamePrefab)
    {
        Debug.Log("Starting mini game...");

        Time.timeScale = 0f;
        MiniGame miniGame = Instantiate(miniGamePrefab);
        miniGame.StartGame(OnMiniGameCompleted);
    }

    private void OnMiniGameCompleted(bool success)
    {
        Time.timeScale = 1f;
        if (success)
        {
            Debug.Log("Mini game completed successfully!");
        }
        else
        {
            Debug.Log("Mini game failed!");
        }
    }
}
