using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;

public class InventrySaveLoad : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        
         MMEventManager.TriggerEvent(new MMGameEvent("Load"));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
           // print(CharacterStatsManager.i.characterStats.Attack);
    }
}
