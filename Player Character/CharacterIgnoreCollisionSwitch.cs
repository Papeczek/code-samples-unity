using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class CharacterIgnoreCollisionSwitch : MonoBehaviour
{
    [Header("Use with Feedbacks: Ability Start / Ability Stop")]
    [Space(30)]
    [SerializeField] private string ignoreCollisionLayerName;
    private int defaultLayer;
    private int ignoreCollLayer;
    private void Start()
    {
        defaultLayer = gameObject.layer;
        ignoreCollLayer = LayerMask.NameToLayer(ignoreCollisionLayerName);
    }
    public void StartDisableCollision()
    {
        gameObject.layer = ignoreCollLayer;
    }
    public void FinishEnableCollision()
    {
        gameObject.layer = defaultLayer;
    }
}
