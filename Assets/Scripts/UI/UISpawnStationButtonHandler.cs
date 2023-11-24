using UnityEngine;
using UnityEngine.EventSystems;

public class UISpawnStationButtonHandler : MonoBehaviour, IPointerDownHandler
{
    private Station _station;
    
    private void Awake() => 
        _station = FindObjectOfType<Station>();
    
    public void OnPointerDown(PointerEventData eventData) => 
        _station?.SpawnStationHadle();
}