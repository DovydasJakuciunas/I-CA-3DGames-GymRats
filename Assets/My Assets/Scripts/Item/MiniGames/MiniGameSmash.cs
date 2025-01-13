using GD.Tweens;
using UnityEngine;

public class MiniGameSmash : MiniGame
{
    private int smashCount = 0;
    private int targetCount = 10;
    private System.Action<bool> onComplete;

    public override int StaminaCost => 30; 

    [SerializeField]
    private LetterTween letterA;

    [SerializeField]
    private LetterTween letterD;



    private bool isAPressed = true;

    public override void StartGame(System.Action<bool> onComplete)
    {
        this.onComplete = onComplete;

        // Start with letter A
        letterA?.StartTween();
    }

    private void Update()
    {
        // Check for input and trigger the corresponding tween
        if (isAPressed && Input.GetKeyDown(KeyCode.A))
        {
            HandleSmash(letterA, letterD);
        }
        else if (!isAPressed && Input.GetKeyDown(KeyCode.D))
        {
            HandleSmash(letterD, letterA);
        }
    }

    private void HandleSmash(LetterTween pressedLetter, LetterTween nextLetter)
    {
        smashCount++;

        // Stop the current animation and reset the pressed letter
        pressedLetter?.OnLetterPressed();

        // Start the next letter's animation
        nextLetter?.StartTween();

        // Swap the active letter
        isAPressed = !isAPressed;

        // Check if the game is completed
        if (smashCount >= targetCount)
        {
            onComplete?.Invoke(true);
            smashCount = 0;

            // Stop all animations and reset letters
            letterA?.StopAnimation();
            letterD?.StopAnimation();
        }
    }
}
