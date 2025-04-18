using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ApplicationResolution : MonoBehaviour
{
    public enum RenderRes
    {
        _Native,
        _1440p,
        _1080p,
        _720p
    }
    public static ApplicationResolution Instance;
    public static Camera MainCamera;
    [Header("Resolution Settings")]
    public RenderRes maxRenderSize = RenderRes._720p;
    public bool variableResolution;
    [Range(0f, 1f)]
    public float axisBias = 0.5f;
    public float minScale = 0.5f;
    //public Framerate targetFramerate = Framerate._30;
    //private float currentDynamicScale = 1.0f;
    private float maxScale = 1.0f;

    // Use this for initialization
    private void Awake()
    {
        Initialize();
        SetRenderScale(Camera.main);
    }
 
    private void Initialize()
    {
        Instance = this;
        Application.targetFrameRate = 300;
    }
   
    private void SetRenderScale(Camera cam)
    {
        float res;
        switch (maxRenderSize)
        {
            case RenderRes._720p:
                res = 1280f;
                break;
            case RenderRes._1080p:
                res = 1920f;
                break;
            case RenderRes._1440p:
                res = 2560f;
                break;
            default:
                res = cam.pixelWidth;
                break;
        }

        var renderScale = Mathf.Clamp(res / cam.pixelWidth, 0.1f, 1.0f);
       // var renderScale = Mathf.Clamp(10, 0.1f, 1.0f);
        maxScale = renderScale;
    #if !UNITY_EDITOR
        UniversalRenderPipeline.asset.renderScale = renderScale;
    #endif
    }

    //private void Update()
    //{
    //    if (!MainCamera) return;

    //    if (variableResolution)
    //    {
    //        MainCamera.allowDynamicResolution = true;

    //        var offset = 0f;
    //        var currentFrametime = Time.deltaTime;
    //        var rate = 0.1f;

    //        switch (targetFramerate)
    //        {
    //            case Framerate._30:
    //                offset = currentFrametime > (1000f / 30f) ? -rate : rate;
    //                break;
    //            case Framerate._60:
    //                offset = currentFrametime > (1000f / 60f) ? -rate : rate;
    //                break;
    //            case Framerate._120:
    //                offset = currentFrametime > (1000f / 120f) ? -rate : rate;
    //                break;
    //        }

    //        currentDynamicScale = Mathf.Clamp(currentDynamicScale + offset, minScale, 1f);

    //        var offsetVec = new Vector2(Mathf.Lerp(1, currentDynamicScale, Mathf.Clamp01((1 - axisBias) * 2f)),
    //            Mathf.Lerp(1, currentDynamicScale, Mathf.Clamp01(axisBias * 2f)));

    //        ScalableBufferManager.ResizeBuffers(offsetVec.x, offsetVec.y);
    //    }
    //    else
    //    {
    //        MainCamera.allowDynamicResolution = false;
    //    }
    //}
}
