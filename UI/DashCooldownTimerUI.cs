using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using DG.Tweening;
public class DashCooldownTimerUI : SkillCooldownTimerUI
{
    [Tooltip("Used for setting cooldown")]
    [SerializeField] private CharacterDash3D characterDash;
    public override void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == GameEvents.DASH_START)
        {
            currentCooldown = 0;
            imageMaterial.DOKill();
            imageMaterial.SetFloat(glowProperty, 1f);
            timerOn = true;
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        float smallDelay = 0.1f;
        maxCooldown = characterDash.Cooldown.PauseOnEmptyDuration + smallDelay;
    }
}
