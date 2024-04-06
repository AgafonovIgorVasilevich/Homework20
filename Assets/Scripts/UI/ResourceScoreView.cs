using UnityEngine;
using TMPro;

public class ResourceScoreView : MonoBehaviour
{
    [SerializeField] private TextAnimator _animator;
    [SerializeField] private TMP_Text _text;

    private ResourceScore _score;

    private void OnDisable() => _score.Changed -= Show;

    public void Initialize(ResourceScore score)
    {
        _score = score;
        _score.Changed += Show;
    }

    private void Show(int score)
    {
        _text.text = score.ToString();
        _animator.Hihglight();
    }
}