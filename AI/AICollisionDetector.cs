using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoreMountains.Tools;

public class AICollisionDetector : MonoBehaviour
{
    public event Action OnCollide;

    [SerializeField] private AIBrain aiBrain;
    [Space]
    [SerializeField] private string stateName = "Charge";
    [SerializeField] private List<string> obstacleLayerName;

    private bool alreadyHit = false;
    private void Start()
    {
        alreadyHit = false;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (aiBrain == null || aiBrain.CurrentState == null) return;
        if (aiBrain.CurrentState.StateName != stateName) return;
        if (obstacleLayerName.Contains(LayerMask.LayerToName(hit.gameObject.layer)))
        {
            if (alreadyHit) return;

            OnCollide?.Invoke();
            alreadyHit = true;
        }
    }
    private void Update()
    {
        if (aiBrain == null || aiBrain.CurrentState == null) return;
        if (aiBrain.CurrentState.StateName!= stateName)
        {
            if (alreadyHit)
            {
                alreadyHit = false;
            }
        }
    }
}
