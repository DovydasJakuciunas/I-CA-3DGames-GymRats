using GD.Tweens;
using UnityEngine;

public class MiniGameSmash : MiniGame
{
    private int smashCount = 0;
    private int targetCount = 10;
    private System.Action<bool> onComplete;

    [SerializeField]
    private LetterTween letterA;

    [SerializeField]
    private LetterTween letterD;

    private bool isAPressed = true;

    public override void StartGame(System.Action<bool> onComplete)
    {
        this.onComplete = onComplete;
        Debug.Log("Smash A+D started");

        // Start with letter A
        letterA?.StartTween();
    }

    private void Update()
    {
        // Check for input and trigger the corresponding tween
        if (isAPressed && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A pressed");
            HandleSmash(letterA, letterD);
        }
        else if (!isAPressed && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D pressed");
            HandleSmash(letterD, letterA);
        }
    }

    private void HandleSmash(LetterTween pressedLetter, LetterTween nextLetter)
    {
        smashCount++;
        Debug.Log($"Smash count: {smashCount}");

        // Stop the current animation and reset the pressed letter
        pressedLetter?.OnLetterPressed();

        // Start the next letter's animation
        nextLetter?.StartTween();

        // Swap the active letter
        isAPressed = !isAPressed;

        // Check if the game is completed
        if (smashCount >= targetCount)
        {
            Debug.Log("Smash A+D completed!");
            onComplete?.Invoke(true);
            smashCount = 0;

            // Stop all animations and reset letters
            letterA?.StopAnimation();
            letterD?.StopAnimation();
        }
    }
}
