using TMPro;
using UnityEngine;

public class UICountResources : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private TMP_Text _countResourcesText;

    private string _resourcesText;
    
    private void Start()
    {
        _resourcesText = _countResourcesText.text;
        
        OnChangeCountResources();
        
        _station.CountResourcesUpdate += OnChangeCountResources;
    }

    private void OnDestroy() => 
        _station.CountResourcesUpdate -= OnChangeCountResources;

    private void OnChangeCountResources() => 
        _countResourcesText.text = _resourcesText + _station.CountResources;
}