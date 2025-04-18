using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using EnhancedUI.EnhancedScroller;
using DG.Tweening;
public class SkillScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    public event Action<SkillData> OnSkillUnlocked;
    public event Action<SkillData> OnSkillUnlockedFailed;
    public event Action OnScrollerScrolled;

    //test for skill cell view
    public Ease ease;
    public float duration = 1f;
    //test
    [Header("SO for first time init")]
    [SerializeField] private PassiveSkillsDataSO meleeSkillsDataSO;
    [SerializeField] private PassiveSkillsDataSO deffenseSkillsDataSO;
    [SerializeField] private PassiveSkillsDataSO rangeSkillsDataSO;
    [Space(20)]
    [field: SerializeField] public List<SkillData> MeleeData;
    [field: SerializeField] public List<SkillData> DeffenseData;
    [field: SerializeField] public List<SkillData> RangeData;
    [Space(15)]
    [SerializeField] private EnhancedScroller MeleeScroller;
    [SerializeField] private EnhancedScroller DeffenseScroller;
    [SerializeField] private EnhancedScroller RangeScroller;

    public ScrollerScrolledDelegate scrollerScrolledDelegate;

    [SerializeField] private SkillCellView skillCellView;
    public static SkillScrollerController i { get; private set; }

    private const string meleeDataSaveFileName = "meleeData";
    private const string deffenseDataSaveFileName = "deffenseData";
    private const string rangeDataSaveFileName = "rangeData";

    private const string saveFolderName = "SkillsSave";
    private const string fileExtension = "sdata";
    private void Awake()
    {
        if (i != null && i != this)
           Destroy(this);
        
        else
            i = this;
        InitializeData();
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        InitializeScrollers();
    }
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if (scroller == MeleeScroller)
        {
            return MeleeData.Count;
        }
        else if (scroller == DeffenseScroller)
        {
            return DeffenseData.Count;
        }
        else
            return RangeData.Count;

    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 300f;
    }
    private void ScrollerScrolled(EnhancedScroller scroller, Vector2 val, float scrollPosition)
    {
        //if (scroller == MeleeScroller)
        //{
        //    DeffenseScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //    RangeScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //}
        //else if (scroller == DeffenseScroller)
        //{
        //    MeleeScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //    RangeScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //}
        //else
        //{
        //    MeleeScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //    DeffenseScroller.ScrollPosition = scroller.NormalizedScrollPosition * scroller.ScrollSize;
        //}
        OnScrollerScrolled?.Invoke(); 
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {

        SkillCellView cellView = scroller.GetCellView(skillCellView) as SkillCellView;
        //print(scroller.name);
        if (scroller == MeleeScroller)
        {
            cellView.SetData(MeleeData[cellIndex]);
        }
        else if (scroller == DeffenseScroller)
        {
            cellView.SetData(DeffenseData[cellIndex]);
        }
        else
        {
            cellView.SetData(RangeData[cellIndex]);
        }
        return cellView;
    }
    private void InitializeScrollers()
    {
        print("Initialize scrollers");
        MeleeScroller.Delegate = this;
        DeffenseScroller.Delegate = this;
        RangeScroller.Delegate = this;

        MeleeScroller.scrollerScrolled = ScrollerScrolled;
        DeffenseScroller.scrollerScrolled = ScrollerScrolled;
        RangeScroller.scrollerScrolled = ScrollerScrolled;

        MeleeScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
        DeffenseScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
        RangeScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
        // RangeScroller.SetScrollPositionImmediately(4100);

        // MeleeScroller.lookAheadAfter = 1000;
        // MeleeScroller.lookAheadBefore = 1000;
        MeleeScroller.ReloadData();
        DeffenseScroller.ReloadData();
        RangeScroller.ReloadData();

        MeleeScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
        DeffenseScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
        RangeScroller.ScrollPosition = 1 * MeleeScroller.ScrollSize;
    }

    public void UnlockSkill(SkillData skillData)
    {
        bool unlocked = false;
        switch (skillData.skillCategory)
        {
            case SkillData.SkillCategory.Melee:

                unlocked = CheckDataIndex(MeleeData);
                break;

            case SkillData.SkillCategory.Deffense:

                unlocked = CheckDataIndex(DeffenseData);
                break;

            case SkillData.SkillCategory.Range:

                unlocked = CheckDataIndex(RangeData);
                break;
        }

        if (unlocked)
        {

        }
        else
        {

        }
       
        bool CheckDataIndex(List<SkillData> dataList)
        {
            int id = dataList.IndexOf(skillData);
            //if data found
            if (id != -1)
            {
                if (dataList[id].canBeUnlocked)
                {
                    //if can be unlocked, unlock it and fire an event
                    dataList[id].unlocked = true;
                  //  Debug.Log("Unlocked skill with id " + id + " from category " + skillData.skillCategory.ToString());;
                    OnSkillUnlocked?.Invoke(dataList[id]);
                    //If this is not last skill in list, unlock next in order
                    if(id != 0)
                    {
                        dataList[id - 1].canBeUnlocked = true;
                        print(dataList);
                    }
                    SaveSkillsData();
                        return true;
                }
                else
                {
                 //   Debug.Log("This skill with id " + id + " can't be unlocked yet");
                    OnSkillUnlockedFailed?.Invoke(dataList[id]);
                    return false;
                }
            }
            else
            {
                Debug.LogError("DATA NOT FOUND");
                return false;
            }
        }
    }
    
    private void InitializeData()
    {
        if (!PlayerPrefs.HasKey("FirstLogIn"))
        {
            InitializeDataForFirstTime();
        }
        else
        {
            LoadSkillsData();
        }
    }
    private void InitializeDataForFirstTime()
    {
        if (PlayerPrefs.HasKey("FirstLogIn")) return;
        //load data from SO
        MeleeData = meleeSkillsDataSO.skillDataList.ConvertAll(data => new SkillData(data));
        DeffenseData = deffenseSkillsDataSO.skillDataList.ConvertAll(data => new SkillData(data));
        RangeData = rangeSkillsDataSO.skillDataList.ConvertAll(data => new SkillData(data));


        //assign category
        SetCategoryForData(MeleeData, SkillData.SkillCategory.Melee);
        SetCategoryForData(DeffenseData, SkillData.SkillCategory.Deffense);
        SetCategoryForData(RangeData, SkillData.SkillCategory.Range);

        //First ones can be unlocked
        MeleeData[0].canBeUnlocked = true;
        DeffenseData[0].canBeUnlocked = true;
        RangeData[0].canBeUnlocked = true;

        //Reverse order for scroll where first item is last
        MeleeData.Reverse();
        DeffenseData.Reverse();
        RangeData.Reverse();

        SaveSkillsData();
        PlayerPrefs.SetInt("FirstLogIn", 1);

        void SetCategoryForData(List<SkillData> skillData, SkillData.SkillCategory skillCategory)
        {
            for (int i = 0; i < skillData.Count; i++)
            {
                skillData[i].skillCategory = skillCategory;
            }
        }
    }

    private void SaveSkillsData()
    {
        MMSaveLoadManager.Save(MeleeData, meleeDataSaveFileName + fileExtension, saveFolderName);
        MMSaveLoadManager.Save(DeffenseData, deffenseDataSaveFileName + fileExtension, saveFolderName);
        MMSaveLoadManager.Save(RangeData, rangeDataSaveFileName + fileExtension, saveFolderName);
    }
    private void LoadSkillsData()
    {
        MeleeData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), meleeDataSaveFileName + fileExtension, saveFolderName);
        DeffenseData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), deffenseDataSaveFileName + fileExtension, saveFolderName);
        RangeData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), rangeDataSaveFileName + fileExtension, saveFolderName);
        print("Loaded Data from binary");
    }
}
