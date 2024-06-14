using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Screens.PresentScreen
{
    public class StudentItem : MonoBehaviour
    {
        [field:SerializeField] public TMP_Text Name { get; private set; }
        
        [SerializeField] private Image bgImage;
        [SerializeField] private Button selfButton;

        private readonly Color _presentColor = new(0.045f, 0.95f, 0, 0.7f);
        private readonly Color _missingColor = new(0.955f, 0.065f, 0, 0.7f);

        public bool IsPresent { get; private set; }

        private void Awake()
        {
            selfButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            IsPresent = !IsPresent;
            bgImage.color = IsPresent ? _presentColor : _missingColor;
        }
    }
}
