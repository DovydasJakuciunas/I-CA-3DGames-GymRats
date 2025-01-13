using DG.Tweening;
using UnityEngine;

namespace GD.Tweens
{
    public class LetterTween : BaseTween
    {
        [SerializeField]
        [Tooltip("The RectTransform of the letter to animate")]
        private RectTransform letter;

        [SerializeField]
        [Tooltip("The target scale for the tween")]
        private float targetScale = 0.5f;

        [SerializeField]
        [Tooltip("The original scale to reset the letter")]
        private float originalScale = 1f;

        private Tween currentTween; // Store the current tween
        private bool isAnimating = false;

        public bool IsAnimating => isAnimating;

        public override void StartTween()
        {
            if (letter == null)
            {
                Debug.LogError("Letter RectTransform is not assigned!");
                return;
            }

            if (!isAnimating)
            {
                isAnimating = true;

                // Create a tween to shrink the letter
                currentTween = letter.DOScale(targetScale, DurationSecs)
                    .SetEase(EaseFunction)
                    .SetDelay(DelaySecs)
                    .SetLoops(LoopCount, LoopType)
                    .SetUpdate(true) // Ensure the tween respects unscaled time
                    .OnComplete(() =>
                    {
                        isAnimating = false;
                    });
            }
        }

        /// <summary>
        /// Stops the animation and resets the letter immediately.
        /// </summary>
        public void StopAnimation()
        {
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill(); // Stop the current tween immediately
                ResetLetter(); // Reset the letter to its original state
            }
        }

        /// <summary>
        /// Reset the letter back to its original scale.
        /// </summary>
        public void ResetLetter()
        {
            letter.localScale = Vector3.one * originalScale; // Reset the scale to the original size
            isAnimating = false;
        }

        /// <summary>
        /// Handles the button press event.
        /// </summary>
        public void OnLetterPressed()
        {
            if (isAnimating)
            {
                StopAnimation(); // Stop the current animation immediately
            }
        }
    }
}
