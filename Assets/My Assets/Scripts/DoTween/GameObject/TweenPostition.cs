using DG.Tweening;
using GD.Tweens;
using Sirenix.OdinInspector;
using UnityEngine;

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
            transform.DOMove(originalPosition + positionDelta, DurationSecs)
                .SetDelay(DelaySecs)
                    .SetEase(EaseFunction)
                        .SetLoops(LoopCount, LoopType)
                            .OnComplete(TweenComplete);
        }

        public override void TweenComplete()
        {
            base.TweenComplete();
        }
    }
}

