using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TheraBytes.BetterUi;
using DG.Tweening;
public class SkillCellView : EnhancedScrollerCellView
{
    public SkillData skillData;
    [Space]
    public Stat statType;
    public bool isSpecialSkill;
    public float skillValue;
    [Space]
    public Sprite iconSprite;
    public BetterImage regularSkillIconImage;
    public BetterImage specialSkillIconImage;
    [Space]
    public string skillTitle;
    public string skillDescription;
    [Space]
    public GameObject regularSkillCell;
    public GameObject specialSkillCell;
    [Space]
    public PopUPUI popUp;
    [Space(20), Header("Grayscale Animation")]
    public Ease ease;
    public float duration = 1f;
    private GrayScaleAnimator bgAnimator;
    private GrayScaleAnimator iconAnimator;
    private void Start()
    {
        SkillScrollerController.i.OnSkillUnlocked += UnlockSkill;
    }
    private void OnDestroy()
    {
        SkillScrollerController.i.OnSkillUnlocked -= UnlockSkill;
    }
    public void SetData(SkillData data)
    {
        skillData = data;
        isSpecialSkill = data.isSpecialAbility;
        statType = data.statType;
        skillValue = data.skillValue;
        skillTitle = data.skillTitle;
        skillDescription = data.skillDescription;

        iconSprite = Resources.Load<Sprite>(data.GetIconSprite());
        popUp.SetPopUpData(data);
        SetSkill();
        StopAnimations();
        SetUnlockedStatus();
    }
    public void UnlockSkill(SkillData skillData)
    {
        if (skillData != this.skillData) return;
        popUp.Unlock();
        popUp.Hide();
        AnimateGrayScale(1f, 0f, SkillScrollerController.i.duration,SkillScrollerController.i.ease);
        CharacterStatsManager.i.SkillUnlocked(skillData);
    }
    private void SetUnlockedStatus()
    {
        
        if (skillData.unlocked)
        {
            AnimateGrayScale(1f, 0f, 0, SkillScrollerController.i.ease);
        }
        else
        {
            AnimateGrayScale(0f, 1f, 0, SkillScrollerController.i.ease);
        }
    }
    private void AnimateGrayScale(float startValue, float targetValue, float duration, Ease ease)
    {
       // if(bgAnimator == null)
       // {
            BetterImage bgImage = isSpecialSkill ? specialSkillCell.GetComponent<BetterImage>() : regularSkillCell.GetComponent<BetterImage>();
            bgAnimator = new GrayScaleAnimator(bgImage, startValue);
        //}

        bgAnimator.AnimateAmount(targetValue, duration, ease);

//        if(iconAnimator == null)
 //       {
            if (isSpecialSkill)
            {
                iconAnimator = new GrayScaleAnimator(specialSkillIconImage, startValue);
            }
            else
            {
                iconAnimator = new GrayScaleAnimator(regularSkillIconImage, startValue);
            }
   //     }
            iconAnimator.AnimateAmount(targetValue, duration, ease);
    }
    private void StopAnimations()
    {
        if (bgAnimator == null || iconAnimator == null) return;
        bgAnimator.KillTween();
        iconAnimator.KillTween();
    }
    private void SetSkill()
    {
        if (isSpecialSkill)
        {
            specialSkillCell.SetActive(true);
            specialSkillIconImage.overrideSprite = iconSprite;

            regularSkillCell.SetActive(false);
        }
        else
        {
            regularSkillCell.SetActive(true);
            regularSkillIconImage.overrideSprite = iconSprite;

            specialSkillCell.SetActive(false);

        }
    }
    //Used by unlock button
    public void UnlockRequest()
    {
        SkillScrollerController.i.UnlockSkill(skillData);
    }
}
