using UnityEngine;
using UnityEngine.EventSystems;

public class UICreateUnitButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StationWallet _stationWallet;
    
    public void OnPointerDown(PointerEventData eventData) => 
        _stationWallet.SpawnUnitHadle();
}