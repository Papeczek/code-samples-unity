using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheraBytes.BetterUi;
using DG.Tweening;
[System.Serializable]
public class GrayScaleAnimator
{
    private BetterImage img;
    private float currentAmount;
    private Tween tween;
    private const int GRAYSCALE = 0;
    public GrayScaleAnimator(BetterImage img, float currentAmount)
    {
        this.img = img;
        this.currentAmount = currentAmount;
    }

    public void AnimateAmount(float targetValue, float duration, Ease easeType)
    {
        if (img != null)
        {
            
         tween = DOTween.To(() => currentAmount, x => currentAmount = x, targetValue, duration)
                .OnUpdate(() => img.SetMaterialProperty(GRAYSCALE, currentAmount))
                .SetEase(easeType);
        }
    }
    public void SetAmountImmediately(float targetValue)
    {
        img.SetMaterialProperty(GRAYSCALE, targetValue);
    }
    
    public void KillTween()
    {
        if (tween != null)
        {
            tween.Kill(); 
        }
    }
}
