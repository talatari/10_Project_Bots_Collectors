using TMPro;
using UnityEngine;

public class UICountResources : MonoBehaviour
{
    [SerializeField] private StationWallet stationWallet;
    [SerializeField] private TMP_Text _countResourcesText;
    [SerializeField] private GameObject _buttonCreateUnit;
    [SerializeField] private GameObject _buttonCreateStation;

    private string _resourcesText;
    
    private void Awake()
    {
        _resourcesText = _countResourcesText.text;
        
        _buttonCreateUnit.SetActive(false);
        _buttonCreateStation.SetActive(false);
        
        OnChangeCountResources();
        
        stationWallet.CountResourcesUpdate += OnChangeCountResources;
        stationWallet.EnoughResourcesForUnit += OnActiveButtonCreateUnit;
        stationWallet.NotEnoughResourcesForUnit += OnInActiveButtonCreateUnit;
        stationWallet.EnoughResourcesForStation += OnActiveButtonCreateStationWallet;
        stationWallet.NotEnoughResourcesForStation += OnInActiveButtonCreateStationWallet;
    }

    private void OnDestroy()
    {
        stationWallet.CountResourcesUpdate -= OnChangeCountResources;
        stationWallet.EnoughResourcesForUnit -= OnActiveButtonCreateUnit;        
        stationWallet.NotEnoughResourcesForUnit += OnInActiveButtonCreateUnit;
        stationWallet.EnoughResourcesForStation += OnActiveButtonCreateStationWallet;
        stationWallet.NotEnoughResourcesForStation += OnInActiveButtonCreateStationWallet;
    }

    private void OnChangeCountResources() => 
        _countResourcesText.text = _resourcesText + stationWallet.CountResources;

    private void OnActiveButtonCreateUnit() => 
        _buttonCreateUnit.SetActive(true);
    
    private void OnInActiveButtonCreateUnit() => 
        _buttonCreateUnit.SetActive(false);
    
    private void OnActiveButtonCreateStationWallet() => 
        _buttonCreateStation.SetActive(true);
    
    private void OnInActiveButtonCreateStationWallet() => 
        _buttonCreateStation.SetActive(false);
}