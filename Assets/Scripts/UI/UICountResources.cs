using TMPro;
using UnityEngine;

public class UICountResources : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private TMP_Text _countResourcesText;
    [SerializeField] private GameObject _buttonCreateUnit;
    [SerializeField] private GameObject _buttonCreateStation;

    private string _resourcesText;
    
    private void Start()
    {
        _resourcesText = _countResourcesText.text;
        
        _buttonCreateUnit.SetActive(false);
        _buttonCreateStation.SetActive(false);
        
        OnChangeCountResources();
        
        _station.CountResourcesUpdate += OnChangeCountResources;
        _station.EnoughResourcesForUnit += OnActiveButtonCreateUnit;
        _station.NotEnoughResourcesForUnit += OnInActiveButtonCreateUnit;
        _station.EnoughResourcesForStation += OnActiveButtonCreateStation;
        _station.NotEnoughResourcesForStation += OnInActiveButtonCreateStation;
    }

    private void OnDestroy()
    {
        _station.CountResourcesUpdate -= OnChangeCountResources;
        _station.EnoughResourcesForUnit -= OnActiveButtonCreateUnit;        
        _station.NotEnoughResourcesForUnit += OnInActiveButtonCreateUnit;
        _station.EnoughResourcesForStation += OnActiveButtonCreateStation;
        _station.NotEnoughResourcesForStation += OnInActiveButtonCreateStation;
    }

    private void OnChangeCountResources() => 
        _countResourcesText.text = _resourcesText + _station.CountResources;

    private void OnActiveButtonCreateUnit() => 
        _buttonCreateUnit.SetActive(true);
    
    private void OnInActiveButtonCreateUnit() => 
        _buttonCreateUnit.SetActive(false);
    
    private void OnActiveButtonCreateStation() => 
        _buttonCreateStation.SetActive(true);
    
    private void OnInActiveButtonCreateStation() => 
        _buttonCreateStation.SetActive(false);
}