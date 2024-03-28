using UnityEngine;
using TMPro;

public class ResourceScoreView : MonoBehaviour
{
    [SerializeField] private ResourceScore _score;
    [SerializeField] private TMP_Text _text;

    private void OnEnable() => _score.Changed += Show;

    private void OnDisable() => _score.Changed -= Show;

    private void Show(int score) => _text.text = score.ToString();
}