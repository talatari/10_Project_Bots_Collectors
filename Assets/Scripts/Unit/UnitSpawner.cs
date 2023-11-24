using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Station _station;
    [SerializeField] private StationWallet _stationWallet;

    private void OnEnable()
    {
        _stationWallet.SpawnUnit += OnSpawn;
    }

    private void OnDisable()
    {
        _stationWallet.SpawnUnit += OnSpawn;
    }

    private void OnSpawn()
    {
        Vector3 position = _station.transform.position;
        Unit unit = Instantiate(_unitPrefab, position, Quaternion.identity);
        unit.transform.parent = gameObject.transform;
        unit.SetStationPosition(position);
    }
}