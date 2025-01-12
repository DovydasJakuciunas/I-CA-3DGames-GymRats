using TMPro;
using UnityEngine;

public class MiniGameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject miniGameUI; // Reference to the mini-game UI object

    [SerializeField]
    private TMP_Text titleText;

    public static MiniGameUIManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure this is a singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Disable the mini-game UI at the start
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the mini-game completion event
        if (MiniGameManager.Instance != null)
        {
            MiniGameManager.Instance.OnMiniGameCompleted += HandleMiniGameComplete;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (MiniGameManager.Instance != null)
        {
            MiniGameManager.Instance.OnMiniGameCompleted -= HandleMiniGameComplete;
        }
    }

    public void ShowMiniGameUI(string title)
    {
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(true); // Enable the mini-game UI
        }

        if (titleText != null)
        {
            titleText.text = title; // Dynamically set the title
        }
    }

    public void HideMiniGameUI()
    {
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(false); // Disable the mini-game UI
        }
    }

    private void HandleMiniGameComplete(bool success)
    {
        Debug.Log($"Mini-game completed. Success: {success}");

        // Hide the mini-game UI after completion
        HideMiniGameUI();

        // Add additional logic here, e.g., show success/failure messages
        if (success)
        {
            Debug.Log("Congratulations! You completed the mini-game.");
        }
        else
        {
            Debug.Log("Better luck next time!");
        }
    }
}
