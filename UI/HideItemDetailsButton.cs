using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
public class HideItemDetailsButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InventoryDetails invDetails;
    private void Awake()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(() =>invDetails.DisplayDetails(null));
       
    }
}
