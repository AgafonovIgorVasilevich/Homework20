using Unity.AI.Navigation;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private BuildingPool _buildingPool;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private LoaderPool _loaderPool;
    [SerializeField] private FactoryList _statsTable;
    [SerializeField] private NavMeshSurface _surface;

    private Building _building;

    public void Build(Vector3 position)
    {
        if (_building != null)
        {
            _building.transform.position = position;
            return;
        }

        _building = _buildingPool.Get();
        _building.transform.position = position;
        _building.Initialize(_surface, _resourcePool, _loaderPool, _statsTable);
        _building.BuildCompleted += ResetBuilding;
    }

    private void ResetBuilding()
    {
        _building.BuildCompleted -= ResetBuilding;
        _buildingPool.Put(_building);
        _building = null;
    }
}