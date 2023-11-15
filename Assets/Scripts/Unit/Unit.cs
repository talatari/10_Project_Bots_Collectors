using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMovement _unitMovement;

    private void OnValidate() => _unitMovement ??= GetComponent<UnitMovement>();

    public bool IsWork { get; private set; }
    
    public void Clear() => Destroy(gameObject);

    public void AssignWork(Vector3 target)
    {
        _unitMovement.Set(target);
        IsWork = true;
    }
    
}