using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using DG.Tweening;
public class SkillMenuViewManager : MonoBehaviour
{
    //testing
    public Ease ease;
    public float duration = 1f;
    public float scale = 1.1f;
    //end testing
    public event Action<SkillData.SkillCategory, int> OnSkillUnlocked;
    public event Action<SkillData> OnSkillUnlockedFailed;

    [SerializeField] private SkillCellViewReworked skillCellView;

    [Header("SO for first time init")]
    [SerializeField] private PassiveSkillsDataSO meleeSkillsDataSO;
    [SerializeField] private PassiveSkillsDataSO defenseSkillsDataSO;
    [SerializeField] private PassiveSkillsDataSO rangeSkillsDataSO;

    [Space(20)]
    [field: SerializeField] public List<SkillData> MeleeData;
    [field: SerializeField] public List<SkillData> DefenseData;
    [field: SerializeField] public List<SkillData> RangeData;

    [SerializeField] private Transform MeleeContentScrollContainer;
    [SerializeField] private Transform DefenseContentScrollContainer;
    [SerializeField] private Transform RangeContentScrollContainer;

    private const string meleeDataSaveFileName = "meleeData";
    private const string deffenseDataSaveFileName = "deffenseData";
    private const string rangeDataSaveFileName = "rangeData";

    private const string saveFolderName = "SkillsSave";
    private const string fileExtension = "sdata";

    public static SkillMenuViewManager i { get; private set; }
    private void Awake()
    {
        if (i != null && i != this)
            Destroy(this);

        else
            i = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();

        }
    }
    private void Start()
    {
        InitializeData();
        SpawnSkillCells();
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

                unlocked = CheckDataIndex(DefenseData);
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
                      Debug.Log("Unlocked skill with id " + id + " from category " + skillData.skillCategory.ToString());;
                    OnSkillUnlocked?.Invoke(dataList[id].skillCategory, id);
                    //If this is not last skill in list, unlock next in order
                    if (id != dataList.Count)
                    {
                        dataList[id + 1].canBeUnlocked = true;
                    }
                    SaveSkillsData();
                    return true;
                }
                else
                {
                       Debug.Log("This skill with id " + id + " can't be unlocked yet");
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
    private void SpawnSkillCells()
    {
        SpawnCells(MeleeData, MeleeContentScrollContainer);
        SpawnCells(DefenseData, DefenseContentScrollContainer);
        SpawnCells(RangeData, RangeContentScrollContainer);
        
        void SpawnCells(List<SkillData> SkillDataList, Transform parentTransform)
        {
            if(parentTransform.childCount > 0)
            {
                foreach (Transform child in parentTransform)
                {
                    Destroy(child.gameObject);
                }
            }
            for (int i = 0; i < SkillDataList.Count; i++)
            {
                SkillCellViewReworked cell = Instantiate(skillCellView, parentTransform);
                cell.SetData(SkillDataList[i]);
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
        DefenseData = defenseSkillsDataSO.skillDataList.ConvertAll(data => new SkillData(data));
        RangeData = rangeSkillsDataSO.skillDataList.ConvertAll(data => new SkillData(data));


        //assign category
        FinalizeDataProperties(MeleeData, SkillData.SkillCategory.Melee);
        FinalizeDataProperties(DefenseData, SkillData.SkillCategory.Deffense);
        FinalizeDataProperties(RangeData, SkillData.SkillCategory.Range);

        //First ones can be unlocked
        MeleeData[0].canBeUnlocked = true;
        DefenseData[0].canBeUnlocked = true;
        RangeData[0].canBeUnlocked = true;

        //Reverse order for scroll where first item is last
       // MeleeData.Reverse();
       // DefenseData.Reverse();
       //RangeData.Reverse();

        SaveSkillsData();
        PlayerPrefs.SetInt("FirstLogIn", 1);

    
        void FinalizeDataProperties(List<SkillData> skillData, SkillData.SkillCategory skillCategory)
        {
            //sets category and id
            for (int i = 0; i < skillData.Count; i++)
            {
                skillData[i].skillCategory = skillCategory;
                skillData[i].id = i;
            }
            //lock all skills but first one
            for (int i = 1; i < skillData.Count; i++)
            {
                skillData[i].canBeUnlocked = false;
                skillData[i].unlocked = false;
            }
        }
        
    }
    public bool IsNextIdUnlocked(SkillData skillData)
    {
        if (skillData.id + 1 >= MeleeData.Count) return false;
        if(skillData.skillCategory == SkillData.SkillCategory.Melee)
        {
            return MeleeData[skillData.id + 1].unlocked;
        }
        else if (skillData.skillCategory == SkillData.SkillCategory.Deffense)
        {
            return DefenseData[skillData.id + 1].unlocked;
        }
        else 
        {
            return RangeData[skillData.id + 1].unlocked;
        }
    }
    private void SaveSkillsData()
    {
        MMSaveLoadManager.Save(MeleeData, meleeDataSaveFileName + fileExtension, saveFolderName);
        MMSaveLoadManager.Save(DefenseData, deffenseDataSaveFileName + fileExtension, saveFolderName);
        MMSaveLoadManager.Save(RangeData, rangeDataSaveFileName + fileExtension, saveFolderName);
    }
    private void LoadSkillsData()
    {
        MeleeData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), meleeDataSaveFileName + fileExtension, saveFolderName);
        DefenseData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), deffenseDataSaveFileName + fileExtension, saveFolderName);
        RangeData = (List<SkillData>)MMSaveLoadManager.Load(typeof(List<SkillData>), rangeDataSaveFileName + fileExtension, saveFolderName);
        print("Loaded Data from binary");
    }
}
