using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class OnTriggerExitOnDestroyHandler : MonoBehaviour
{
    private const string PlAYER_TAG = "Player";
    private bool isInTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlAYER_TAG))
        {
            isInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(PlAYER_TAG))
        {
            isInTrigger = false;
        }
    }
    public void ExitTriggerOnDestroy()
    {
        //if (isInTrigger)
        //{
            MMGameEvent.Trigger("TriggerDestroyed");
        //    isInTrigger = false;
        //}
    }

    
}
