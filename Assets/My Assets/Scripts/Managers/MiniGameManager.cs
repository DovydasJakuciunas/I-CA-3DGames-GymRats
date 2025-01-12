using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    [SerializeField]
    private StaminaManager stamina;

    private MiniGame currentMiniGame;

    // Event to notify listeners about mini-game completion
    public event Action<bool> OnMiniGameCompleted;

    private void Awake()
    {
        gameObject.SetActive(true);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMiniGame(MiniGame miniGamePrefab, Transform playerTransform, float activationRange)
    {
        // Check if the player is within range
        if (Vector3.Distance(playerTransform.position, miniGamePrefab.transform.position) > activationRange)
        {
            Debug.Log("Player is not within range to start the mini-game.");
            return;
        }

        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame.gameObject); // Destroy the existing instance
        }

        // Activate the mini-game
        Time.timeScale = 0f; // Pause the game
        MiniGameUIManager.Instance?.ShowMiniGameUI(miniGamePrefab.name);
        currentMiniGame = Instantiate(miniGamePrefab);
        currentMiniGame.gameObject.SetActive(true); // Ensure the mini-game is active
        currentMiniGame.StartGame(HandleMiniGameCompletion);
    }

    private void HandleMiniGameCompletion(bool success)
    {
        // Resume the game after the mini-game ends
        Time.timeScale = 1f;

        // Notify listeners of the mini-game's result
        OnMiniGameCompleted?.Invoke(success);

        if (success)
        {
            stamina.UseStamina(30);
        }

        // Hide the mini-game UI and clean up the current mini-game
        MiniGameUIManager.Instance?.HideMiniGameUI();

        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame.gameObject);
            currentMiniGame = null;
        }
    }
}
