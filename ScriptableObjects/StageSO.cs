using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageSO : ScriptableObject
{
    public int id;

    public Vector3 instantiateOffset;

    public GameObject stagePrefab;

    public stageType StageType;
    public enum stageType
    {
        Reward,
        Small,
        Big,
        Boss
    }

}
