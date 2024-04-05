using UnityEngine;
using System;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private ResourceScore _score;
    [SerializeField] private int _factoryCost = 5;
    [SerializeField] private int _loaderCost = 3;

    private UpgradeTarget _target = UpgradeTarget.Loader;

    public Vector3 BuildingPosition { get; private set; }

    public event Action LoadredBuyed;
    public event Action FactoryBuyed;

    private void OnEnable() => _score.Increased += CheckAbleBuy;

    private void OnDisable() => _score.Increased -= CheckAbleBuy;

    public void SetBuildingPriority(Vector3 position)
    {
        _target = UpgradeTarget.Factory;
        BuildingPosition = position;
    }

    private void CheckAbleBuy()
    {
        switch (_target)
        {
            case UpgradeTarget.Loader:
                BuyLoader();
                break;

            case UpgradeTarget.Factory:
                BuyFactory();
                break;
        }
    }

    private void BuyLoader()
    {
        if (_score.IsPaid(_loaderCost))
            LoadredBuyed?.Invoke();
    }

    private void BuyFactory()
    {
        if(_score.IsPaid(_factoryCost))
        {
            _target = UpgradeTarget.Loader;
            FactoryBuyed?.Invoke();
        }
    }
}