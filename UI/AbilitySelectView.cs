using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
public class AbilitySelectView : MonoBehaviour,MMEventListener<MMGameEvent>
{
    [SerializeField] private SkillTemplateUI crossbowTemplate;
    [SerializeField] private SkillTemplateUI swordTemplate;
    [SerializeField] private SkillTemplateUI generalTemplate;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject containerGO;

    [SerializeField] private JoystickInputEnabler joystickInputEnabler;

    private SkillSO crossbowSkillSO;
    private SkillSO swordSkillSO;
    private SkillSO generalSkillSO;
    private void Start()
    {
        containerGO.SetActive(false);
        canvasGroup.DOFade(0f, 0f);
    }
    private void Show()
    {
        float fadeDuration = 1f;
        joystickInputEnabler.DisableJoystick();
        containerGO.SetActive(true);
        AssignAbilities();
        canvasGroup.DOFade(1, fadeDuration).SetUpdate(true);
        MMTimeManager.Instance.SetTimeScaleTo(0f);
    }
    private void Hide()
    {
        float fadeDuration = 0f;
        containerGO.SetActive(false);
        canvasGroup.DOFade(0f, fadeDuration);
        MMTimeManager.Instance.SetTimeScaleTo(1f);
        joystickInputEnabler.EnableJoystick();
    }
    private void AssignAbilities()
    {
        crossbowSkillSO = CharacterSkillManager.i.GetNextSkillSOFromList(CharacterSkillManager.SkillCategory.Crossbow);
        swordSkillSO = CharacterSkillManager.i.GetNextSkillSOFromList(CharacterSkillManager.SkillCategory.Sword);
        generalSkillSO = CharacterSkillManager.i.GetRandomGeneralSkill();

        OverrideTemplate(crossbowTemplate, crossbowSkillSO);
        OverrideTemplate(swordTemplate, swordSkillSO);
        OverrideTemplate(generalTemplate, generalSkillSO);
    }
    private void OverrideTemplate(SkillTemplateUI skillTemplateUI, SkillSO skillSO)
    {
        skillTemplateUI.skillIcon.sprite = skillSO.icon;
        skillTemplateUI.description.text = skillSO.skillDescription;
        skillTemplateUI.skillName.text = skillSO.skillName;
    }
    
    public void OnMMEvent(MMGameEvent eventType)
    {
       if(eventType.EventName == GameEvents.PICK_ABILITY)
        {
            Show();
        }
       if(eventType.EventName == GameEvents.ABILITY_PICKED)
        {
            Hide();
        }
    }
    public SkillSO GetSkillSO(SkillPath skillPath)
    {
        if (skillPath == SkillPath.Crossbow)
            return crossbowSkillSO;
        else if (skillPath == SkillPath.Sword)
            return swordSkillSO;
        else if (skillPath == SkillPath.General)
            return generalSkillSO;
        else
            return null;
    }
    public enum SkillPath
    {
        Crossbow,
        Sword,
        General
    }
    private void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }
    private void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
}
