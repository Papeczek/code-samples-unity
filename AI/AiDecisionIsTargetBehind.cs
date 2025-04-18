using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class AiDecisionIsTargetBehind : AIDecision
{
    public CharacterOrientation3D characterOrientation3D;
    public override bool Decide()
    {
        return CheckIfTargetIsBehind(_brain.Target.position, transform.position);
    }

    protected bool CheckIfTargetIsBehind(Vector3 targetPosition, Vector3 referencePosition)
    {
        if (_brain.Target == null)
        {
            return false;
        }
        Vector3 heading = targetPosition - referencePosition;
        float dot = Vector3.Dot(heading,characterOrientation3D.ModelDirection);
       // Debug.Log(dot < 0);
        return dot < 0;
    }

}
