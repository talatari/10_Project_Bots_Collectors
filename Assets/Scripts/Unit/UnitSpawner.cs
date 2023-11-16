using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _countUnit = 3;

    private UnitCollection _unitCollection;
    private Station _station;

    private void OnValidate() => _station ??= GetComponent<Station>();

    public void Initialize(UnitCollection unitCollection) => _unitCollection = unitCollection;
    
    public void Spawn()
    {
        for (int i = 0; i < _countUnit; i++)
        {
            Unit unit = Instantiate(_unitPrefab, Vector3.zero, Quaternion.identity);
            _unitCollection.Add(unit);

            unit.transform.parent = gameObject.transform;
            unit.SetBasePosition(_station.transform);
        }
    }
    
    
}