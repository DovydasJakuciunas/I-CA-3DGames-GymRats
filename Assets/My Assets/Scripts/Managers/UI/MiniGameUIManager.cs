using GD;
using TMPro;
using UnityEngine;

public class MiniGameUIManager : Singleton<MiniGameUIManager>
{
    [SerializeField]
    private GameObject miniGameUI; // Reference to the mini-game UI object

    [SerializeField]
    private TMP_Text titleText; // Text component for the mini-game title

    protected override void Awake()
    {
        // Call base Singleton's Awake to handle instance initialization
        base.Awake();

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

    /// <summary>
    /// Show the mini-game UI with the given title.
    /// </summary>
    /// <param name="title">The title to display on the mini-game UI.</param>
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

    /// <summary>
    /// Hide the mini-game UI.
    /// </summary>
    public void HideMiniGameUI()
    {
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(false); // Disable the mini-game UI
        }
    }

    /// <summary>
    /// Handle the mini-game completion event.
    /// </summary>
    /// <param name="success">Whether the mini-game was successfully completed.</param>
    private void HandleMiniGameComplete(bool success)
    {
        // Hide the mini-game UI after completion
        HideMiniGameUI();
    }
}
