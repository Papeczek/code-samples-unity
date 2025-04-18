using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleSettingsViewButtonUI : MonoBehaviour
{
    [SerializeField] private Button button; 
    private void Start()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(SettingsManager.i.ToggleSettings);
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
