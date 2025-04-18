using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class AIActionDash3D : AIAction
{
    public float dashDelay = 0f;
    [Space(25)]
    [Tooltip("Adjust dash distance to player position")]
    public bool adjustDistance = false;
    public float dashDistanceOffset = 0;

    protected Character _character;
    protected CharacterDash3D _characterDash;

    private bool alreadyDashed = false;
    

    public override void Initialization()
    {
        if (!ShouldInitialize) return;
        base.Initialization();
        _character = this.gameObject.GetComponentInParent<Character>();
        _characterDash = _character?.FindAbility<CharacterDash3D>();
    }
    public override void PerformAction()
    {
        if (alreadyDashed) return;
           StartCoroutine(Dash());
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        alreadyDashed = false;
    }
    public IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDelay);
        if (adjustDistance)
        {
            AdjustDashDistance();
        }
        _characterDash.DashStart();
        alreadyDashed = true;
    }
    public void AdjustDashDistance()
    {
        if (_brain.Target != null)
        {
            float maxDistance = 15;
            float distance = Vector3.Distance(transform.position, _brain.Target.position) + dashDistanceOffset;
            if (distance > maxDistance)
                distance = maxDistance;
            
            _characterDash.DashDistance = distance;
        }
    }
}
