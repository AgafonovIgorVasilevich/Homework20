using UnityEngine;
using System;

public class ResourceScore : MonoBehaviour
{
    [SerializeField] private Stock _stock;

    private int _score;

    public event Action<int> Changed;
    public event Action Increased;

    private void OnEnable() => _stock.ResourceReceived += Add;

    private void OnDisable() => _stock.ResourceReceived -= Add;

    public bool IsPaid(int price)
    {
        if (price > _score)
            return false;

        _score -= price;
        Changed?.Invoke(_score);
        return true;
    }

    private void Add()
    {
        _score++;
        Changed?.Invoke(_score);
        Increased?.Invoke();
    }
}