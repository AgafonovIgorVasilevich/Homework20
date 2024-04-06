using UnityEngine.UI;
using UnityEngine;

public class FactoryList : MonoBehaviour
{
    [SerializeField] private RectTransform _scrollerPanel;
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _view;
    [SerializeField] private Slider _scroller;

    public void AddItem(FactoryStats item) => item.transform.SetParent(_content, false);

    public void UpdateScroller()
    {
        ShowScroller();
        ResetScoller();
    }

    private void ShowScroller()
    {
        if (_content.rect.height >= _view.rect.height)
            _scrollerPanel.gameObject.SetActive(true);
        else
            _scrollerPanel.gameObject.SetActive(false);
    }

    private void ResetScoller()
    {
        if (_scrollView.verticalNormalizedPosition != _scroller.value)
            _scroller.value = _scrollView.verticalNormalizedPosition;
    }
}