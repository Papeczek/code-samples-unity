using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class AIRotationSpeedAdjust : MonoBehaviour
{
    [SerializeField] private AIBrain aiBrain;
    [SerializeField] private CharacterOrientation3D charOrientation;
    [SerializeField] private float checkInterval = 0.3f;
    [SerializeField] private float normalRotationSpeed = 8f;
    [SerializeField] private float mediumRotationSpeed = 3f;
    [SerializeField] private float closeRotationSpeed = 0.7f;
    [Space]
    [SerializeField] private float closeDistance = 5f;
    [SerializeField] private float mediumDistance = 8f;
    [Space]
    [SerializeField] private string ignoreSpeedAdjustStateName;
    private float timer = 0f;
    private void Awake()
    {
        if(aiBrain == null)
        {
            aiBrain = GetComponent<AIBrain>();
        }
        if(charOrientation == null)
        {
            Debug.LogError("CharacterOrientation3D not assigned! " + transform.parent.name);
        }
        timer = 0f;
    }
    
    void Update()
    {
        if (!aiBrain.isActiveAndEnabled) return;

        if(aiBrain.CurrentState.StateName == ignoreSpeedAdjustStateName)
        {
            charOrientation.RotateToFaceMovementDirectionSpeed = normalRotationSpeed;
            return;
        }
            
        timer += Time.deltaTime;
        if(timer >= checkInterval)
        {
          var distance = Vector3.Distance(transform.position, aiBrain.Target.position);

          if(distance <= closeDistance)
          {
            charOrientation.RotateToFaceMovementDirectionSpeed = closeRotationSpeed;     
          }
          if(distance > closeDistance && distance <= mediumDistance)
          {
            charOrientation.RotateToFaceMovementDirectionSpeed = mediumRotationSpeed;     
          }
          if (distance > mediumDistance)
          {
            charOrientation.RotateToFaceMovementDirectionSpeed = normalRotationSpeed;
          }
            timer = 0f;
        }
    }
}
