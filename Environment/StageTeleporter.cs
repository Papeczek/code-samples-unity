using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class StageTeleporter : Teleporter
{
    private new void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SetNewDestination());
    }
    private IEnumerator SetNewDestination()
    {
        float delay = .1f;
        yield return new WaitForSeconds(delay);
        Destination = StageManager.i.currDestination;
    }
}
