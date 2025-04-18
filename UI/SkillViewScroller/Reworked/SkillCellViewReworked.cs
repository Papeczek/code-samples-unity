using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheraBytes.BetterUi;
using TMPro;
public class SkillCellViewReworked : MonoBehaviour
{
    //Data
    public SkillData skillData { get; private set;}
    private int id;

    private Sprite iconSprite;
    private string skillTitle;
    private string skillDescription;
    private SkillData.SkillCategory skillCategory;

    private Stat statType;
    private float skillValue;

    private bool canBeUnlocked;
    private bool unlocked;
    private bool connectorActivated;

    //Refferences
    [SerializeField] private SkillCellDataSetter lockedCellViewSetter;
    [SerializeField] private SkillCellDataSetter unlockableCellViewSetter;
    [SerializeField] private SkillCellDataSetter unlockedCellViewSetter;

    [SerializeField] private SkillCellViewVisuals skillCellViewVisuals;
    private void Start()
    {
        SkillMenuViewManager.i.OnSkillUnlocked += OnSkillUnlocked;
    }
    public void SetData(SkillData data)
    {
        //Cache all the data
        skillData = data;
        statType = data.statType;
        skillValue = data.skillValue;
        skillTitle = data.skillTitle;
        skillDescription = data.skillDescription;
        canBeUnlocked = data.canBeUnlocked;
        unlocked = data.unlocked;
        id = data.id;
        skillCategory = data.skillCategory;

        iconSprite = Resources.Load<Sprite>(data.GetIconSprite());

        //Check locked status of the cell and set the data
        skillCellViewVisuals.SetCategoryColor(data.skillCategory);
        SetCellCorrectLockedStatus(data).SetCellView(data, iconSprite);
        skillCellViewVisuals.SetConnectorStatus(SkillMenuViewManager.i.IsNextIdUnlocked(data));

    }
    public void OnSkillUnlocked(SkillData.SkillCategory skillCategory, int id)
    {
        if(skillCategory == this.skillCategory && id == this.id)
        {
            unlockedCellViewSetter.SetCellView(skillData, iconSprite);
            skillCellViewVisuals.UnlockSkill();
        }
        if (skillCategory == this.skillCategory && id + 1 == this.id)
        {
            unlockableCellViewSetter.SetCellView(skillData, iconSprite);
            skillCellViewVisuals.StartCoroutine(skillCellViewVisuals.SetSkillUnlockable());
        }
        if (skillCategory == this.skillCategory && id - 1 == this.id)
        {
            skillCellViewVisuals.StartCoroutine(skillCellViewVisuals.ActivateConnector());
        }
    }
    
    private SkillCellDataSetter SetCellCorrectLockedStatus(SkillData data)
    {
        //Checks if the cell has locked/unlockable/unlocked status
        lockedCellViewSetter.gameObject.SetActive(false);
        unlockableCellViewSetter.gameObject.SetActive(false);
        unlockedCellViewSetter.gameObject.SetActive(false);

        if (!data.canBeUnlocked)
        {
            lockedCellViewSetter.gameObject.SetActive(true);
            return lockedCellViewSetter;
        }
        if (data.canBeUnlocked && !data.unlocked)
        {
            unlockableCellViewSetter.gameObject.SetActive(true);
            return unlockableCellViewSetter;
        }
        if (data.unlocked)
        {
            unlockedCellViewSetter.gameObject.SetActive(true);
            skillCellViewVisuals.SetAllCollors();
            return unlockedCellViewSetter;
        }
        
        else return null;
    }

    //Used by unlock button
    public void UnlockRequest()
    {
        SkillMenuViewManager.i.UnlockSkill(skillData);
    }
}
