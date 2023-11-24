using UnityEngine;
using UnityEngine.EventSystems;

public class UICreateStationButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StationWallet _stationWallet;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _stationWallet.CreateStationHadle();
    }
}