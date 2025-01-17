using DG.Tweening;
using GD.Tweens;
using Sirenix.OdinInspector;
using UnityEngine;

//Code By Niall McGuinness

namespace MyAssets.Tweens
{
    public class TweenPostition : BaseTween
    {
        #region Fields - Inspector
        [TabGroup("Transformation")]
        [SerializeField]
        [Tooltip("The amount the object moves")]
        private Vector3 positionDelta = Vector3.up;

        #endregion

        #region Fields - Internal

        private Vector3 originalPosition;

        #endregion


        public override void Start()
        {
            originalPosition = transform.position;

            base.Start();
        }

        public override void StartTween()
        {
            transform.DOMove(originalPosition + positionDelta, DurationSecs)            // Move the object to the new position
                .SetDelay(DelaySecs)                                                    // Set the delay before the tween starts
                    .SetEase(EaseFunction)                                              // Set the ease function
                        .SetLoops(LoopCount, LoopType)                                  // Set the loop count and type 
                            .OnComplete(TweenComplete);                                 // Call the tween complete method
        }

        public override void TweenComplete()
        {
            base.TweenComplete();
        }
    }
}

