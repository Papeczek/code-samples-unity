using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterStageTeleporterFeedback : MonoBehaviour
{
    public void StageManager_SwapStages()
    {
        StageManager.i.SwapStagesStartCoroutine();
    }
}
