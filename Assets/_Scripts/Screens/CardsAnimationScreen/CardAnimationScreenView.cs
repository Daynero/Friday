using System;
using _Scripts.Data;
using _Scripts.ScreenNavigationSystem;
using _Scripts.Screens.QuestionScreen.QuestionPanels;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace _Scripts.Screens.CardsAnimationScreen
{
    public class CardAnimationScreenView : ScreenBase
    {
        [SerializeField] private Transform container;
        [SerializeField] private CardWithNumber cardWithNumber;
        [SerializeField] private ScrollToElement scrollToElement;
        [SerializeField] private float scrollDuration = 1f; // Duration for the scrolling animation
        [SerializeField] private ScrollRect scrollRect; // ScrollRect component

        private void Awake()
        {

        }

        private void Start()
        {
            Invoke(nameof(ScrollToTargetCard), 1f);
        }

        private void ScrollToTargetCard()
        {
            if (scrollToElement.TargetElement != null)
            {
                RectTransform targetRectTransform = scrollToElement.TargetElement;
                RectTransform containerRectTransform = container as RectTransform;

                if (containerRectTransform != null)
                {
                    // Calculate the target position in the container's local space
                    float targetPositionX = -targetRectTransform.anchoredPosition.x;

                    // Convert the target position to a normalized value between 0 and 1
                    float normalizedPosition = Mathf.Clamp01(targetPositionX / containerRectTransform.rect.width);

                    // Use DoTween to animate the horizontal scroll
                    scrollRect.DOHorizontalNormalizedPos(normalizedPosition, scrollDuration).SetEase(Ease.InOutQuad);
                }
            }
        }

        private void GoToNextScreen()
        {
            ScreenNavigationSystem.ScreenNavigationSystem.Instance.NavigateTo(ScreenName.CardAnimation);
        }

        public override void Show(object data = null)
        {
        }

        public override void Hide()
        {
        }
    }
}
