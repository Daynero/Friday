using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToElement : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private Button button;
    
    
    private float scrollDuration = 5;

    private float _offset;
    
    public RectTransform TargetElement { get; set; }

    private void Awake()
    {
        button.onClick.AddListener(ScrollToTargetCard);
    }

    private void Start()
    {
        _offset = horizontalLayoutGroup.padding.left;
    }

    private void ScrollToTargetCard()
    {
        
    }
}