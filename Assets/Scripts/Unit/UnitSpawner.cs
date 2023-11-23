using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _countUnit = 3;

    private UnitCollection _unitCollection;
    private Station _station;

    private void Start()
    {
        for (int i = 0; i < _countUnit; i++)
            SpawnUnit();
    }

    public void Initialize(UnitCollection unitCollection, Station station)
    {
        _unitCollection = unitCollection;
        _station = station;
    }

    public void SpawnUnit()
    {
        Unit unit = Instantiate(_unitPrefab, _station.transform.position, Quaternion.identity);
        _unitCollection.Add(unit, _station);

        unit.transform.parent = gameObject.transform;
        unit.SetStationPosition(_station.transform);
    }
}