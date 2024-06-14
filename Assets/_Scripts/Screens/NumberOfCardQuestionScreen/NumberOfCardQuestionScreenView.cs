using _Scripts.Data;
using _Scripts.ScreenNavigationSystem;
using _Scripts.Screens.QuestionScreen.QuestionPanels;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Screens.NumberOfCardQuestionScreen
{
    public class NumberOfCardQuestionScreenView : ScreenBase
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private TMP_InputField inputField;

        private bool isAnimating = false;

        private void Awake()
        {
            nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            if (!isAnimating)
            {
                nextButton.interactable = false; 
                if (int.TryParse(inputField.text, out int number))
                {
                    if (number >= 1 && number <= 72)
                    {
                        ScreenNavigationSystem.ScreenNavigationSystem.Instance.NavigateTo(ScreenName.CardAnimation);
                    }
                    else
                    {
                        ShakeInputField();
                    }
                }
                else
                {
                    ShakeInputField();
                }
            }
        }

        private void ShakeInputField()
        {
            isAnimating = true;
            inputField.transform.DOShakePosition(0.5f, 10, 20)
                .OnComplete(() =>
                {
                    inputField.text = "";
                    nextButton.interactable = true;
                    isAnimating = false;
                });
        }

        public override void Show(object data = null)
        {

        }

        public override void Hide()
        {

        }
    }
}
