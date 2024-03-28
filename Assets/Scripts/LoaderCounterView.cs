using UnityEngine;
using TMPro;

public class LoaderCounterView : MonoBehaviour
{
    [SerializeField] private LoaderSpawner _spawner;
    [SerializeField] private TMP_Text _text;

    private void OnEnable() => _spawner.CarCountChanged += Show;

    private void OnDisable() => _spawner.CarCountChanged -= Show;

    private void Show(int current, int max) => _text.text = $"{current}/{max}";
}