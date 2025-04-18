using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
public class PlayerCharacter : Character
{
    [field:SerializeField]public new GameObject CameraTarget { get; set;}
	public new Vector3 CameraTargetStartingPosition { get; private set; }
    protected override void Initialization()
    {
		if (this.gameObject.MMGetComponentNoAlloc<TopDownController2D>() != null)
		{
			CharacterDimension = CharacterDimensions.Type2D;
		}
		if (this.gameObject.MMGetComponentNoAlloc<TopDownController3D>() != null)
		{
			CharacterDimension = CharacterDimensions.Type3D;
		}

		// we initialize our state machines
		MovementState = new MMStateMachine<CharacterStates.MovementStates>(gameObject, SendStateChangeEvents);
		ConditionState = new MMStateMachine<CharacterStates.CharacterConditions>(gameObject, SendStateChangeEvents);

		// we get the current input manager
		SetInputManager();
		// we store our components for further use 
		CharacterState = new CharacterStates();
		_controller = this.gameObject.GetComponent<TopDownController>();
		if (CharacterHealth == null)
		{
			CharacterHealth = this.gameObject.GetComponent<Health>();
		}

		CacheAbilitiesAtInit();
		if (CharacterBrain == null)
		{
			CharacterBrain = this.gameObject.GetComponent<AIBrain>();
		}

		if (CharacterBrain != null)
		{
			CharacterBrain.Owner = this.gameObject;
		}

		Orientation2D = FindAbility<CharacterOrientation2D>();
		Orientation3D = FindAbility<CharacterOrientation3D>();
		_characterPersistence = FindAbility<CharacterPersistence>();

		AssignAnimator();

		// instantiate camera target
		if (CameraTarget == null)
		{
			CameraTarget = new GameObject();
			CameraTarget.transform.SetParent(this.transform);
			CameraTarget.transform.localPosition = Vector3.zero;
			CameraTarget.name = "CameraTarget";
			print("Created new Camera Target for Player");
		}
		CameraTargetStartingPosition = CameraTarget.transform.localPosition;
		

		if (LinkedInputManager != null)
		{
			if (OptimizeForMobile && LinkedInputManager.IsMobile)
			{
				if (this.gameObject.MMGetComponentNoAlloc<MMConeOfVision2D>() != null)
				{
					this.gameObject.MMGetComponentNoAlloc<MMConeOfVision2D>().enabled = false;
				}
			}
		}
	}



}
