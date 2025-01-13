using System;
using UnityEngine;

public class MiniGameTiming : MiniGame
{
    [Header("Timing Mini-Game Settings")]
    [SerializeField]
    private Transform movingObject;

    [SerializeField]
    private Transform targetZone;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float targetZoneWidth = 1f;

    private bool isGameActive;
    private Action<bool> onCompleteCallback;

    public override int StaminaCost => 25;

    private void Update()
    {
        if (!isGameActive)
            return;

        // Use unscaled time for the PingPong calculation
        float movement = Mathf.PingPong(Time.unscaledTime * moveSpeed, targetZoneWidth) - (targetZoneWidth / 2);
        movingObject.localPosition = new Vector3(movement, movingObject.localPosition.y, movingObject.localPosition.z);

        // Debugging the movement position for testing
        Debug.Log($"Movement Position: {movement}");

        // Check for player input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    private void CheckSuccess()
    {
        // Calculate the distance between the moving object and the center of the target zone
        float distance = Mathf.Abs(movingObject.localPosition.x - targetZone.localPosition.x);

        // Success if the moving object is within the bounds of the target zone
        if (distance <= targetZoneWidth / 2)
        {
            Debug.Log("Mini-Game Success!");
            EndGame(true); // Successfully complete the mini-game
        }
        else
        {
            Debug.Log("Mini-Game Failed!");
            EndGame(false); // Mini-game failed
        }
    }

    public override void StartGame(Action<bool> onComplete)
    {
        Debug.Log("Starting Timing Mini-Game!");
        onCompleteCallback = onComplete;
        isGameActive = true;
        onComplete?.Invoke(true);
    }

    private void EndGame(bool success)
    {
        isGameActive = false; // Stop the game logic
        onCompleteCallback?.Invoke(success); // Notify listeners of success or failure

        if (success)
        {
            Debug.Log("Player completed the mini-game successfully!");
        }
        else
        {
            Debug.Log("Player failed the mini-game.");
        }
    }
}
