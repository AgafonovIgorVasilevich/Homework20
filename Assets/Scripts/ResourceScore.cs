using UnityEngine;
using System;

public class ResourceScore : MonoBehaviour
{
    [SerializeField] private Stock _stock;

    private int _score;

    public event Action<int> Changed;

    private void OnEnable() => _stock.ResourceReceived += Add;

    private void OnDisable() => _stock.ResourceReceived -= Add;

    private void Start() => Changed?.Invoke(_score);

    private void Add()
    {
        _score++;
        Changed?.Invoke(_score);
    }
}