using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]

public class Stock : MonoBehaviour
{
    public event Action ResourceReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Loader loader) && loader.State == LoaderState.Full)
        {
            loader.BackToPool();
            ResourceReceived?.Invoke();
        }
    }
}