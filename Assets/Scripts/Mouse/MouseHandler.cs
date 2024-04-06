using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private FlagCursor _flagCursor;
    [SerializeField] private Builder _builder;

    private Upgrader _upgrader;
    private Ray _ray;

    public event Action<Transform> Focused;

    private void Update() => MouseMove();

    private void MouseMove()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            _flagCursor.transform.position = hit.point;
            _flagCursor.SetVisible(_upgrader);

            if (Input.GetMouseButtonDown(0))
            {
                Focused?.Invoke(hit.transform);
                Build(hit.point);
                GetUpgraderFromClick(hit);
            }
        }
    }

    private void GetUpgraderFromClick(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Upgrader upgrader) && upgrader.IsUsed == false)
        {
            _upgrader = upgrader;
            _upgrader.FactoryBuyed += ResetUpgrader;
        }
    }

    private void Build(Vector3 position)
    {
        if (_upgrader == null || _flagCursor.IsCorrect() == false)
            return;

        Focused?.Invoke(_upgrader.transform);
        _upgrader.SetBuildingPriority(position);
        _builder.Build(position);
        ResetUpgrader();
    }

    private void ResetUpgrader()
    {
        if (_upgrader != null)
            _upgrader.FactoryBuyed -= ResetUpgrader;

        _upgrader = null;
    }
}