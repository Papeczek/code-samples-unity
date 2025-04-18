using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
public class AIActionMoveTowardsTarget3DInLookDirection : AIAction
{
    /// the minimum distance from the target this Character can reach.
    [Tooltip("the minimum distance from the target this Character can reach.")]
    public float MinimumDistance = 1f;

    protected Vector3 _directionToTarget;
    protected Vector2 _movementVector;
    protected CharacterMovement _characterMovement;
    protected CharacterOrientation3D _characterOrientation3D;
    public CharacterController _characterController;


    public override void Initialization()
    {
        if (!ShouldInitialize) return;
        base.Initialization();
        _characterMovement = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterMovement>();
        _characterOrientation3D = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterOrientation3D>();
    }
    public override void PerformAction()
    {
        Move();
    }
    protected virtual void Move()
    {
        if (_brain.Target == null)
        {
            return;
        }
       

        _characterMovement.SetMovement(_movementVector);
      
 
        if (Mathf.Abs(this.transform.position.x - _brain.Target.position.x) < MinimumDistance)
        {
            _characterMovement.SetHorizontalMovement(0f);
        }

        if (Mathf.Abs(this.transform.position.z - _brain.Target.position.z) < MinimumDistance)
        {
            _characterMovement.SetVerticalMovement(0f);
        }
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        _directionToTarget = _brain.Target.position - this.transform.position;
        _movementVector.x = _directionToTarget.x;
        _movementVector.y = _directionToTarget.z;
       // _movementVector.x = _characterOrientation3D.ModelDirection.x;
       // _movementVector.y = _characterOrientation3D.ModelDirection.z;
       
        
    }
    public override void OnExitState()
    {
        base.OnExitState();

       
        _characterMovement?.SetHorizontalMovement(0f);
        _characterMovement?.SetVerticalMovement(0f);
    }
   
}
