using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

[System.Serializable]
public class Skill
{
    public bool skillUnlocked = false;

    public GameObject skillIconContainer;
    public SkillCooldownTimerUI skillTimerUI;
    public float timerMax;
    public float skillTimer = 0f;

    public bool skillTimerActive = false;
    public bool skillTimerUIActive =  false;

    public void StartUITimer()
    {
        if (!skillTimerUIActive)
        {
            skillTimerUI.StartTimer(timerMax);
            skillTimerUIActive = true;
        }
    }
}
public class SkillsActivationHandler : MonoBehaviour,MMEventListener<MMGameEvent>
{
    [SerializeField] private Skill powerSlashSkill;
    public static SkillsActivationHandler i { get; private set; }
    private void Awake()
    {
        i = this;
    }

    void Update()
    {
        HandleSkillActivationTiming(powerSlashSkill, GameEvents.POWER_SLASH_READY);
    }
    public void ThirdSlashSkillUnlocked()
    {
        MMGameEvent.Trigger(GameEvents.POWER_SLASH_READY);
        if (!powerSlashSkill.skillUnlocked)
        {
            UnlockSkill(powerSlashSkill);
        }
    }
    public void ThirdSlashStunSkillUnlocked()
    {
        MMGameEvent.Trigger("ThirdSlashStunEnable");
    }
    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == GameEvents.POWER_SLASH_USED)
        {
            powerSlashSkill.skillTimerActive = true;
        }
    }
    private void HandleSkillActivationTiming(Skill skill, string eventName)
    {
        if (skill.skillTimerActive)
        {
            skill.StartUITimer();
            skill.skillTimer += Time.deltaTime;
            if (skill.skillTimer > skill.timerMax)
            {
                MMGameEvent.Trigger(eventName);
                skill.skillTimerActive = false;
                skill.skillTimerUIActive = false;
                skill.skillTimer = 0f;
            }
        }
    }
    private void UnlockSkill(Skill skill)
    {
        skill.skillUnlocked = true;
        skill.skillIconContainer.SetActive(true);
    }
    #region Event Subscription
    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    private void OnDisable()
    {
        this.MMEventStopListening();
    }
    #endregion
}
