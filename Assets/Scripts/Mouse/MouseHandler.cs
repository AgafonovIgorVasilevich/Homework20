using UnityEngine.EventSystems;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private FlagCursor _flagCursor;
    [SerializeField] private Builder _builder;

    private Upgrader _upgrader;
    private Ray _ray;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            ShowFlagCursor(hit);

            if (Input.GetMouseButtonDown(0) == false)
                return;

            if (_flagCursor.IsCorrect())
                Build(hit.point);

            GetUpgraderFromClick(hit);
        }
    }

    private void ShowFlagCursor(RaycastHit hit)
    {
        _flagCursor.gameObject.SetActive(_upgrader != null);
        _flagCursor.transform.position = hit.point;
    }

    private void GetUpgraderFromClick(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Factory factory))
        {
            _upgrader = factory.TryGetUpgrader();

            if (_upgrader != null)
                _upgrader.FactoryBuyed += ResetUpgrader;
        }
    }

    private void Build(Vector3 position)
    {
        if (_upgrader == null)
            return;

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