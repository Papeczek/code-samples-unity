using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;

public class PickAbilityButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AbilitySelectView abilitySelectView;
    public enum SkillPath
    {
        Crossbow,
        Sword,
        General
    }
    public SkillPath skill_Path;
    private void Awake()
    {
        switch (skill_Path)
        {
            case SkillPath.Crossbow:
                button.onClick.AddListener(()=>CharacterSkillManager.i.ActivateSkill(abilitySelectView.GetSkillSO(AbilitySelectView.SkillPath.Crossbow)));
                break;
            case SkillPath.Sword:
                button.onClick.AddListener(()=>CharacterSkillManager.i.ActivateSkill(abilitySelectView.GetSkillSO(AbilitySelectView.SkillPath.Sword)));
                break;
            case SkillPath.General:
                button.onClick.AddListener(()=>CharacterSkillManager.i.ActivateSkill(abilitySelectView.GetSkillSO(AbilitySelectView.SkillPath.General)));
                break;
        }
        button.onClick.AddListener(()=>MMGameEvent.Trigger("AbilityPicked"));
    }
    
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

}
