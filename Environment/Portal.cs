using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Tools;
using DG.Tweening;
public class Portal : MonoBehaviour, MMEventListener<MMGameEvent>
{
    private SphereCollider teleporterCollider;
    private bool activated = true;
    [SerializeField] private bool autoActivate = false;

    [SerializeField] private GameObject portalContainerGO;
    [Space]
    [SerializeField] private MeshRenderer portalBase;
    private Material baseMaterial;
    
    [SerializeField] private MeshRenderer portalWave;
    private Material waveMaterial;

    [SerializeField] private MeshRenderer portalCore;
    private Material coreMaterial;

    static readonly int materialAlpha = Shader.PropertyToID("_AlphaFadeAmount");

    [Space,Header("Activation animation")]
    [SerializeField] private Ease baseEase;
    [SerializeField] private float baseDuration = 1f;
    [Space]
    [SerializeField] private Ease waveEase;
    [SerializeField] private float waveDuration = 1f;
    [Space]
    [SerializeField] private Ease coreEase;
    [SerializeField] private float coreDuration = 1f;
    [Space]
    public UnityEvent OnActivate;
    #region Event Subscription
    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    private void OnDisable()
    {
        this.MMEventStopListening();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
    }
    #endregion
    private void Start()
    {

        if (autoActivate) return;
        baseMaterial = portalBase.material;
        waveMaterial = portalWave.material;
        coreMaterial = portalCore.material;

        baseMaterial.DOFloat(1f, materialAlpha, 0f);
        waveMaterial.DOFloat(1f, materialAlpha, 0f);
        coreMaterial.DOFloat(1f, materialAlpha, 0f);
        portalContainerGO.SetActive(false);

        teleporterCollider = GetComponent<SphereCollider>();
        teleporterCollider.enabled = false;
        activated = false;
    }
    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName== "OnAreaCleared")
        {
            if(portalContainerGO != null)
            {
                Activate();
            }
        }
    }
    public void Activate()
    {
        if (activated) return;
        OnActivate?.Invoke();
        portalContainerGO.SetActive(true);

        baseMaterial.DOFloat(0f, materialAlpha, baseDuration).SetEase(baseEase);
        waveMaterial.DOFloat(0f, materialAlpha, waveDuration).SetEase(waveEase);
        coreMaterial.DOFloat(0f, materialAlpha, coreDuration).SetEase(coreEase);

        teleporterCollider.enabled = true;
        activated = true;

    }
    public void DeActivate()
    {
        Destroy(portalContainerGO,1f);
    }
}
