using TMPro;
using UnityEngine;

namespace _Scripts.Screens.QuestionScreen.QuestionPanels
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ContentPanel : MonoBehaviour
    {
        [field: SerializeField] public TMP_InputField InputField { get; private set; }
        
        public abstract ContentType ContentType { get; }

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = alpha > 0;
            _canvasGroup.blocksRaycasts = alpha > 0;
        }

        public CanvasGroup GetCanvasGroup()
        {
            return _canvasGroup;
        }
    }
}