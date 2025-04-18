using System.Collections;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class StageManager : MonoBehaviour
{
    public bool testing = false;
    [SerializeField] private StageSO[] stagesSO;
  
    [SerializeField] private GameObject stagePresentInScene;

    [SerializeField] private Teleporter rewardDestination;
    [SerializeField] private Teleporter smallDestination;
    [SerializeField] private Teleporter bigDestination;
    [SerializeField] private Teleporter bossDestination;
    public int currentStage { get; private set; } = 0;
    private int lastStage;
    [field: SerializeField,Space] public Teleporter currDestination { get; private set; }
    public static StageManager i { get; private set; }
    private void Awake()
    {
        i = this;
    }
    private IEnumerator Start()
    {
        if (testing) yield break;
        lastStage = stagesSO.Length - 1;
        yield return new WaitForSeconds(.1f);
        InstantiateNewStage(0);
    }
    public void SwapStagesStartCoroutine()
    {
        StartCoroutine(SwapStages());
    }
    private IEnumerator SwapStages()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentStage == lastStage) yield break;
        currentStage++;
        InstantiateNewStage(currentStage);
    }
    private void InstantiateNewStage(int stageIndex)
    {
        Destroy(stagePresentInScene, 0f);

        var newStageSO = stagesSO[stageIndex];
        stagePresentInScene = Instantiate(newStageSO.stagePrefab, newStageSO.instantiateOffset,Quaternion.identity);
        stagePresentInScene.SetActive(true);

        if (stageIndex  == lastStage) return;

        var nextStageSO = stagesSO[stageIndex + 1];
        SetDestinaton(nextStageSO);
    }
    private void SetDestinaton(StageSO stageSO)
    {
        switch (stageSO.StageType)
        {
            case StageSO.stageType.Reward:
                currDestination = rewardDestination;
                break;
            case StageSO.stageType.Small:
                currDestination = smallDestination;
                MMGameEvent.Trigger(GameEvents.ON_SMALL_STAGE);
                break;
            case StageSO.stageType.Big:
                currDestination = bigDestination;
                MMGameEvent.Trigger(GameEvents.ON_BIG_STAGE);
                break;
            case StageSO.stageType.Boss:
                currDestination = bossDestination;
                break;
        }
    }
}
