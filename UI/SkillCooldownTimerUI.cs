using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using DG.Tweening;
public class SkillCooldownTimerUI : MonoBehaviour, MMEventListener<MMGameEvent>
{
    protected bool timerOn = false;
    public float currentCooldown;
    public float maxCooldown;

    protected Material imageMaterial;

    [SerializeField] protected Image cooldownImage;
    [Space]
    [SerializeField] protected Ease animEase = Ease.OutCirc;
    [SerializeField] protected float animDuration = 0.5f;
    [SerializeField] protected float targetGlowAmount = 2f;

    protected static int glowProperty = Shader.PropertyToID("_GlowGlobal");
    protected virtual void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
        currentCooldown = 0;
        //imageMaterial = cooldownImage.materialForRendering;
        imageMaterial = Instantiate(cooldownImage.material);
        cooldownImage.material = imageMaterial;
     //   imageMaterial = cooldownImage.GetComponent<Image>().material;
    }
    protected void OnDestroy()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
    public virtual void OnMMEvent(MMGameEvent eventType)
    {

    }
    protected virtual void Animate()
    {
        imageMaterial.DOFloat(targetGlowAmount, glowProperty, animDuration).SetEase(animEase).SetLoops(2, LoopType.Yoyo);
    }
    public void StartTimer(float maxCooldown)
    {
        currentCooldown = 0f;
        this.maxCooldown = maxCooldown - 0.1f;
        timerOn = true;
    }
    protected virtual void Update()
    {
        if (timerOn)
        {
            currentCooldown += Time.deltaTime;
            cooldownImage.fillAmount = currentCooldown / maxCooldown;
            if (currentCooldown >= maxCooldown)
            {
                Animate();
                timerOn = false;
            }
        }
    }
}
