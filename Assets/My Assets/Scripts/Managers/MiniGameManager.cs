using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    [SerializeField]
    private StaminaManager stamina;

    // Event to notify listeners about mini-game completion
    public event Action<bool> OnMiniGameCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartMiniGame(MiniGame miniGamePrefab)
    {

        Time.timeScale = 0f; // Pause the game
        MiniGameUIManager.Instance?.ShowMiniGameUI(miniGamePrefab.name);
        MiniGame miniGame = Instantiate(miniGamePrefab);
        miniGame.StartGame(HandleMiniGameCompletion);
    }

    private void HandleMiniGameCompletion(bool success)
    {

        // Resume the game after the mini-game ends
        Time.timeScale = 1f;

        // Notify listeners of the mini-game's result
        OnMiniGameCompleted?.Invoke(success);

        if (success)
            stamina.UseStamina(30);
        else
            return;
        // Hide the mini-game UI
        MiniGameUIManager.Instance?.HideMiniGameUI();
    }
}
