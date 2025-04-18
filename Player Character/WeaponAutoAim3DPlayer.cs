using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
public class WeaponAutoAim3DPlayer : WeaponAutoAim3D
{
    public LayerMask CameraTargetLayer;
    public bool isMeleeWeapon = false;
    private int frames = 0;

    protected override void Update()
    {
        base.Update();
        ResetCameraTargetPosition();
    }
    private void OnEnable()
    {
        if (isMeleeWeapon)
            frames = 0;
    }
    protected override void HandleMoveCameraTarget()
    {
        if (!MoveCameraTarget || (Target == null) || (_isOwnerNull))
        {
            return;
        }
        if ((CameraTargetLayer.value & 1 << Target.gameObject.layer) == 0) return;
        
            _newCamTargetPosition = Vector3.Lerp(_weapon.Owner.transform.position, Target.transform.position, CameraTargetDistance);
            _newCamTargetDirection = _newCamTargetPosition - this.transform.position;

            if (_newCamTargetDirection.magnitude > CameraTargetMaxDistance)
            {
                _newCamTargetDirection = _newCamTargetDirection.normalized * CameraTargetMaxDistance;
            }

            _newCamTargetPosition = this.transform.position + _newCamTargetDirection;

            _newCamTargetPosition = Vector3.Lerp(_weapon.Owner.CameraTarget.transform.position,
                _newCamTargetPosition,
                Time.deltaTime * CameraTargetSpeed);

            _weapon.Owner.CameraTarget.transform.position = _newCamTargetPosition;
        
        frames = 0;

    }
    public void ResetCameraTargetPosition()
    {
        float maxFrames = 120;

        if(frames>maxFrames) 
            return;
        //If is melee weapon, always return to initial position
        if (isMeleeWeapon)
            MoveCamera();
        if (!MoveCameraTarget || (_isOwnerNull))
            return;
        
        //If there is no target, return to initial position
        if(Target== null)
            MoveCamera();
        
        //If there is target but it's not a target for CameraMove, return to initial position
        else
        {
            if ((CameraTargetLayer.value & 1 << Target.gameObject.layer) != 0) return;
                MoveCamera();
        }

       void MoveCamera()
       {
            _newCamTargetPosition = Vector3.Lerp(_weapon.Owner.CameraTarget.transform.localPosition, _weapon.Owner.CameraTargetStartingPosition, Time.deltaTime * CameraTargetSpeed * .75f);

            _weapon.Owner.CameraTarget.transform.localPosition = _newCamTargetPosition;
            frames++;
       }
    }
}
