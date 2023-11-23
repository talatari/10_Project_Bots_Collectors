using UnityEngine;
using UnityEngine.EventSystems;

public class UICreateUnitButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Station _station;
    
    public void OnPointerDown(PointerEventData eventData) => 
        _station.CreateUnitHadle();
}