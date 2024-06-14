using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.Tools
{
    public class ButtonScaleEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Button _button;
        private RectTransform _rectTransform;

        void Awake()
        {
            _button = GetComponent<Button>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rectTransform.DOScale(0.95f, 0.15f).SetEase(Ease.OutQuad);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad);
        }
    }
}