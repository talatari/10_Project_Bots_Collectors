using TMPro;
using UnityEngine;

public class UICountResources : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private TMP_Text _countResourcesText;
    [SerializeField] private GameObject _buttonCreateStation;

    private string _resourcesText;
    
    private void Start()
    {
        _resourcesText = _countResourcesText.text;
        _buttonCreateStation.SetActive(false);
        
        OnChangeCountResources();
        
        _station.CountResourcesUpdate += OnChangeCountResources;
        _station.EnoughResources += OnActiveButtonCreateStation;
        _station.NotEnoughResources += OnInActiveButtonCreateStation;
    }

    private void OnDestroy()
    {
        _station.CountResourcesUpdate -= OnChangeCountResources;
        _station.EnoughResources -= OnActiveButtonCreateStation;        
        _station.NotEnoughResources += OnInActiveButtonCreateStation;
    }

    private void OnChangeCountResources() => 
        _countResourcesText.text = _resourcesText + _station.CountResources;

    private void OnActiveButtonCreateStation() => 
        _buttonCreateStation.SetActive(true);
    
    private void OnInActiveButtonCreateStation() => 
        _buttonCreateStation.SetActive(false);
}