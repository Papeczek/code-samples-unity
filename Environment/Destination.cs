using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] private Teleporter teleporter;
    private void Awake()
    {
        if(teleporter== null)
        {
            teleporter = GetComponent<Teleporter>();
            if(teleporter == null)
            {
                Debug.LogError("Teleporter not assigned!");
            }
        }
    }
   
}
