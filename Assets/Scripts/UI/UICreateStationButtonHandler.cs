using UnityEngine;
using UnityEngine.EventSystems;

public class UICreateStationButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Station _station;
    [SerializeField] private GameObject _buttonCreateStation;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _station.CreateStationHadle();
    }
}