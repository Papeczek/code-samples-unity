using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Feedbacks;
public class CharacterSurfaceSoundsWithFeedbacks : CharacterSurfaceSounds
{
    
    [SerializeField] private CharacterAnimationFeedbacks animationFeedbacks;
 
	protected override void HandleSurfaceChange()
	{
		if (_surfaceIndexLastFrame != CurrentSurfaceIndex)
		{
			if (_surfaceIndexLastFrame >= 0 && _surfaceIndexLastFrame < Surfaces.Count)
			{
				Surfaces[_surfaceIndexLastFrame].OnExitSurfaceFeedbacks?.Invoke();
			}
			Surfaces[CurrentSurfaceIndex].OnEnterSurfaceFeedbacks?.Invoke();
			animationFeedbacks.WalkFeedbacks = Surfaces[CurrentSurfaceIndex].WalkFeedback;
			animationFeedbacks.RunFeedbacks = Surfaces[CurrentSurfaceIndex].RunFeedback;
		}
		_surfaceIndexLastFrame = CurrentSurfaceIndex;
	}

}
