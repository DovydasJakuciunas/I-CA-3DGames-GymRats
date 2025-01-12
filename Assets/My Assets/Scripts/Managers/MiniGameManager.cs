using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    // Event to notify listeners about mini-game completion
    public event Action<bool> OnMiniGameCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartMiniGame(MiniGame miniGamePrefab)
    {
        Debug.Log("Starting mini game...");

        Time.timeScale = 0f; // Pause the game
        MiniGameUIManager.Instance?.ShowMiniGameUI(miniGamePrefab.name);
        MiniGame miniGame = Instantiate(miniGamePrefab);
        miniGame.StartGame(HandleMiniGameCompletion);
    }

    private void HandleMiniGameCompletion(bool success)
    {
        Debug.Log(success ? "Mini game completed successfully!" : "Mini game failed.");

        // Resume the game after the mini-game ends
        Time.timeScale = 1f;

        // Notify listeners of the mini-game's result
        OnMiniGameCompleted?.Invoke(success);

        // Hide the mini-game UI
        MiniGameUIManager.Instance?.HideMiniGameUI();
    }
}
