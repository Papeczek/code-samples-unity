using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class PowerSlashUsedEventDelay : MonoBehaviour
{
    [SerializeField] private float delay = 0.7f;
   public void CoroutineStarter()
    {
        StartCoroutine(EventDelay());
    } 
   private IEnumerator EventDelay()
    {
        yield return new WaitForSeconds(delay);
        MMGameEvent.Trigger(GameEvents.POWER_SLASH_USED);
    }
}
