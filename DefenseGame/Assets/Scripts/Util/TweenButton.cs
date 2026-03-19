using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Spellbind
{
    public class TweenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] float tweenScale = 1.1f;
        [SerializeField] float duration = 0.1f;
        [SerializeField] Ease pressEase = Ease.OutBack;
        [SerializeField] Ease releaseEase = Ease.OutBack;

        public UnityEvent onClick = new();
        private Vector3 originalScale;
        private RectTransform rectTransform;

        [SerializeField] bool isLock = false;
        [SerializeField] float power = 10f;
        private float localX = 0f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            originalScale = rectTransform.localScale;

            if (isLock)
                localX = transform.localPosition.x;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isLock)
            {
                LMotion.Shake.Create(localX, power, 0.3f)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                    .BindToLocalPositionX(transform)
                    .AddTo(this);
            }
            else
            {
                LMotion.Create(originalScale, originalScale * tweenScale, duration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                    .WithEase(pressEase)
                    .BindToLocalScale(transform)
                    .AddTo(this);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isLock) return;
            LMotion.Create(rectTransform.localScale, originalScale, duration)
                   .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                   .WithEase(releaseEase)
                   .BindToLocalScale(transform)
                   .AddTo(this);
        }

        public void OnPointerClick(PointerEventData eventData) => onClick?.Invoke();
    }
}
