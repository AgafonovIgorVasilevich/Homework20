using UnityEngine.UI;
using UnityEngine;

public class FactoryList : MonoBehaviour
{
    [SerializeField] private RectTransform _scrollerPanel;
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _view;
    [SerializeField] private Slider _scroller;

    private float _scrolValue;

    private void Update()
    {
        ResetScoller();
        ShowScroller();
    }

    private void ResetScoller()
    {
        if (_scrollView.verticalNormalizedPosition != _scrolValue)
            _scroller.value = _scrollView.verticalNormalizedPosition;

        _scrolValue = _scrollView.verticalNormalizedPosition;
    }

    private void ShowScroller()
    {
        if (_content.rect.height > _view.rect.height)
            _scrollerPanel.gameObject.SetActive(true);
        else
            _scrollerPanel.gameObject.SetActive(false);
    }
}
