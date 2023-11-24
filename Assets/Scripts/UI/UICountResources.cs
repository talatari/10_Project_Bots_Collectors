using TMPro;
using UnityEngine;

public class UICountResources : MonoBehaviour
{
    [SerializeField] private TMP_Text _countResourcesText;
    [SerializeField] private GameObject _buttonSpawnUnit;
    [SerializeField] private GameObject _buttonSpawnStation;

    private StationWallet _stationWallet;
    private string _resourcesText;
    
    private void Awake()
    {
        _stationWallet = FindObjectOfType<StationWallet>();
        
        _resourcesText = _countResourcesText.text;
        
        _buttonSpawnUnit.SetActive(false);
        _buttonSpawnStation.SetActive(false);
        
        OnChangeCountResources();
    }

    private void OnEnable()
    {
        _stationWallet.CountResourcesUpdate += OnChangeCountResources;
        _stationWallet.EnoughResourcesForUnit += OnActiveButtonSpawnUnit;
        _stationWallet.NotEnoughResourcesForUnit += OnInActiveButtonSpawnUnit;
        _stationWallet.EnoughResourcesForStation += OnActiveButtonSpawnStationWallet;
        _stationWallet.NotEnoughResourcesForStation += OnInActiveButtonSpawnStationWallet;
    }

    private void OnDisable()
    {
        _stationWallet.CountResourcesUpdate -= OnChangeCountResources;
        _stationWallet.EnoughResourcesForUnit -= OnActiveButtonSpawnUnit;        
        _stationWallet.NotEnoughResourcesForUnit -= OnInActiveButtonSpawnUnit;
        _stationWallet.EnoughResourcesForStation -= OnActiveButtonSpawnStationWallet;
        _stationWallet.NotEnoughResourcesForStation -= OnInActiveButtonSpawnStationWallet;
    }

    private void OnChangeCountResources() => 
        _countResourcesText.text = _resourcesText + _stationWallet.CountResources;

    private void OnActiveButtonSpawnUnit() => 
        _buttonSpawnUnit.SetActive(true);
    
    private void OnInActiveButtonSpawnUnit() => 
        _buttonSpawnUnit.SetActive(false);
    
    private void OnActiveButtonSpawnStationWallet() => 
        _buttonSpawnStation.SetActive(true);
    
    private void OnInActiveButtonSpawnStationWallet() => 
        _buttonSpawnStation.SetActive(false);
}