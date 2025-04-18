using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using DG.Tweening;

public class LoadingAttackHitboxIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer indicatorRenderer;
    [SerializeField] private Transform indicatorMaskTransform;

    [Range(0,255)]
    [SerializeField] private int targetAlpha;
    [SerializeField] private bool Permitted;

    private Vector3 startingLocalPosition;
    private Color zeroAlpha;
    private void OnDestroy()
    {
        transform.DOKill();
    }
    private void Start()
    {
        zeroAlpha = indicatorRenderer.color - new Color(0f,0f,0f,1f);
        indicatorRenderer.color = zeroAlpha;
        startingLocalPosition = indicatorMaskTransform.localPosition;
    }
    public void ToggleLoadingIndicator(float loadingDuration, bool isLoadingAttack)
    {
        if (!Permitted) return;
        if (isLoadingAttack)
        {
            BeginLoadingIndicator(loadingDuration);
        }
        else
        {
           StartCoroutine(StopLoadingIndicator());
        }
    }
 
    private void BeginLoadingIndicator(float loadingDuration)
    {
        if (indicatorMaskTransform == null || indicatorRenderer == null) return;
        indicatorRenderer.color = zeroAlpha;
        indicatorMaskTransform.localPosition = startingLocalPosition;
        indicatorRenderer.gameObject.SetActive(true);
        indicatorRenderer.DOFade((float)targetAlpha/255, loadingDuration / 3f);
        indicatorMaskTransform.DOBlendableLocalMoveBy(Vector3.forward, loadingDuration / 2f);

    }
    private IEnumerator StopLoadingIndicator()
    {
        if (indicatorMaskTransform == null || indicatorRenderer == null) yield break;
        float blinkTime = 0.2f;
        float timeAfterAttackIsFinished = 0.5f;
        indicatorRenderer.DOFade(1f, blinkTime).SetLoops(2,LoopType.Yoyo);
        yield return new WaitForSeconds(timeAfterAttackIsFinished);
        indicatorRenderer.gameObject.SetActive(false);
    }
}
