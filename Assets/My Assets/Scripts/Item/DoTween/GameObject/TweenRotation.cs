using DG.Tweening;
using GD.Tweens;
using UnityEngine;

//Used Niall McGuinness Code 

namespace MyAssets.Tweens
{
    public class TweenRotation : BaseTween
    {
        [SerializeField]
        private Vector3 rotationAngleTarget = new Vector3(0,0,0);

        private Quaternion originalRotation;
        public override void Start()
        {
            originalRotation = transform.rotation;
            base.Start();
        }
        public override void StartTween()
        {
            transform.DORotate(originalRotation.eulerAngles + rotationAngleTarget, DurationSecs)
                .SetDelay(DelaySecs)
                .SetEase(EaseFunction)
                .SetLoops(LoopCount, LoopType)
                .OnComplete(TweenComplete);
        }
    }
}
