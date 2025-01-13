using System;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    [SerializeField]
    private StaminaManager stamina;

    private MiniGame currentMiniGame;

    // A list to manage all workout UIs
    [SerializeField]
    private List<GameObject> workoutUIs = new List<GameObject>();

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

        Debug.Log("Attempting to start the mini-game...");


        // Check if the player has enough stamina to start the mini-game
        if (stamina.currentStamina < miniGamePrefab.StaminaCost)
        {
            Debug.Log($"Not enough stamina to start the mini-game. Required: {miniGamePrefab.StaminaCost}, Current: {stamina.currentStamina}");
            return;
        }

        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame.gameObject); // Destroy the existing instance
        }

        // Deactivate all workout UIs except the one assigned to the current mini-game
        DeactivateAllWorkoutUIs(miniGamePrefab);

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
            if (currentMiniGame != null)
            {
                // Deduct stamina based on the specific mini-game
                stamina.UseStamina(currentMiniGame.StaminaCost);
                Debug.Log($"Stamina reduced by {currentMiniGame.StaminaCost} for {currentMiniGame.GetType().Name}.");
            }
        }

        // Hide the mini-game UI and clean up the current mini-game
        MiniGameUIManager.Instance?.HideMiniGameUI();

        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame.gameObject);
            currentMiniGame = null;
        }
    }

    private void DeactivateAllWorkoutUIs(MiniGame activeMiniGame)
    {
        foreach (GameObject workoutUI in workoutUIs)
        {
            if (workoutUI != null)
            {
                // Only activate the workout UI associated with the current mini-game
                workoutUI.SetActive(workoutUI.name == activeMiniGame.name);
            }
        }
    }
}
