using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

//Code by : Niall Mc.Guinness

namespace GD.Tweens
{
    public abstract class BaseTween : MonoBehaviour
    {
        #region Fields - Inspector
        [SerializeField]
        [Tooltip("Should the tween start on Start()")]
        private bool isEnabledOnStart = true;

        [TabGroup("Timing")]
        [SerializeField]
        [Range(0.1f, 10f)]
        [Tooltip("The duration of the tween in seconds")]
        private float durationSecs = 1;

        [TabGroup("Timing")]
        [SerializeField]
        [Range(0f, 240f)]
        [Tooltip("The delay before the tween starts in seconds")]
        private float delaySecs = 0;

        [TabGroup("Timing")]
        [SerializeField]
        [Tooltip("The ease function to use for the tween")]
        private Ease easeFunction = Ease.Linear;

        [TabGroup("Loop")]
        [SerializeField]
        [Range(-1, 100)]
        private int loopCount = 0;

        [TabGroup("Loop")]
        [SerializeField]
        [Tooltip("The type of loop to use for the tween")]
        [HideIf("HideIfLoopCount")]
        private LoopType loopType = LoopType.Yoyo;

        [TabGroup("Events")]
        [SerializeField]
        [Tooltip("Event to call when the tween completed")]
        private UnityEvent onComplete;

        #endregion

        #region Properties
        //Getters and Setters
        public bool IsEnabledOnStart { get => isEnabledOnStart; set => isEnabledOnStart = value; }
        public float DurationSecs { get => durationSecs; set => durationSecs = value; }
        public Ease EaseFunction { get => easeFunction; set => easeFunction = value; }
        public int LoopCount { get => loopCount; set => loopCount = value; }
        public LoopType LoopType { get => loopType; set => loopType = value; }
        public UnityEvent OnComplete { get => onComplete; set => onComplete = value; }
        public float DelaySecs { get => delaySecs; set => delaySecs = value; }

        public virtual void Start()
        {
            if (isEnabledOnStart)
            {
                StartTween();
            }

        } 
        #endregion

        /// <summary>
        /// Call for Starting Tween
        /// </summary>
        public virtual void StartTween()
        {
        }

        /// <summary>
        /// Call when Tween is completed
        /// </summary>
        public virtual void TweenComplete()
        {
            OnComplete?.Invoke();
        }

        /// <summary>
        /// Hides the loop type if the loop count is 0 or 1.
        /// </summary>
        /// <returns></returns>
        public virtual bool HideIfLoopCount()
        {
            return LoopCount == 0 || LoopCount == 1;
        }
    }
}